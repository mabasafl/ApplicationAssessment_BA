using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using Application.Authentication.Interface;
using Application.Data.Data;
using Application.Data.Dtos.Auth;
using Application.Data.Models.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Authentication.Services
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _dbContext;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(DataContext dbContext, IConfiguration config,IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _config = config;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<Users> RegisterUser(UserDto user)
        {
            CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var newUser = new Users
            {
                Username = user.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            await _dbContext.Users.AddAsync(newUser);
            await _dbContext.SaveChangesAsync();

            return newUser;
        }

        public async Task<AuthResponseDto> Login(UserDto user)
        {
            var existingUser = await _dbContext.Users.FirstOrDefaultAsync(x => x.Username == user.Username);

            if (existingUser == null)
            {
                return new AuthResponseDto
                {
                    Message = "User not found",
                    Token = string.Empty,
                    Success = false
                };
            }

            bool verifyPassword =
                VerifyPasswordHash(user.Password, existingUser.PasswordHash, existingUser.PasswordSalt);
            if (!verifyPassword)
            {
                return new AuthResponseDto
                {
                    Message = "Wrong Password",
                    Token = string.Empty,
                    Success = false
                };
            }

            string token = CreateToken(existingUser);
            var refreshtoken = CreateRefreshToken();
            SetRefreshToken(refreshtoken, existingUser);
            return new AuthResponseDto
            {
                Message = "User logged in successfully",
                Token = token,
                Success = true,
                RefreshToken = refreshtoken.Token,
                TokenExpires = refreshtoken.Expires
                
            };

        }

        public async Task<AuthResponseDto> RefreshToken()
        {
            var refreshToken = _httpContextAccessor?.HttpContext?.Request.Cookies["refrestToken"];
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);

            if (user == null)
            {
                return new AuthResponseDto
                {
                    Message = "Invalid Refresh Token"
                };
            }
            else if (user.TokenExpires < DateTime.Now)
            {
                return new AuthResponseDto
                {
                    Message = "Token expired"
                };
            }

            string token = CreateToken(user);
            var newRefreshToken = CreateRefreshToken();
            SetRefreshToken(newRefreshToken, user);

            return new AuthResponseDto()
            {
                Success = true,
                Token = token,
                RefreshToken = newRefreshToken.Token,
                TokenExpires = newRefreshToken.Expires
            };
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(Users user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Authentication:Secret_key").Value));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires:DateTime.Now.AddDays(1),
                signingCredentials: credentials
                );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);  
            return jwtToken;

        }

        private RefreshToken CreateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now
            };

            return refreshToken;
        }

        private async Task SetRefreshToken(RefreshToken refreshToken, Users user)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = refreshToken.Expires
            };

            _httpContextAccessor?.HttpContext?.Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);
            user.RefreshToken = refreshToken.Token;
            user.TokenCreated = refreshToken.Created;
            user.TokenExpires = refreshToken.Expires;
            await _dbContext.SaveChangesAsync();
        }
    }
}
