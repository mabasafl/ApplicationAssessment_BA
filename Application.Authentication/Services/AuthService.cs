﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Application.Authentication.Interface;
using Application.Core.Repositories.Interfaces;
using Application.Data.Data;
using Application.Data.Models.Auth;
using Application.DataTransfer.Dtos.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Authentication.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IBaseRepository<Users> _repository;

        public AuthService(IConfiguration config,IHttpContextAccessor httpContextAccessor, IBaseRepository<Users> repository)
        {
            _config = config;
            _httpContextAccessor = httpContextAccessor;
            _repository = repository;
        }
        public async Task<Users> RegisterUser(UserDto user)
        {
            createPasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);
            Users newUser = new Users
            {
                Username = user.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            await _repository.AddAsync(newUser);

            return newUser;
        }

        public async Task<AuthResponseDto> Login(UserDto user)
        {
            Users existingUser = await _repository.GetByNameAsync(x => x.Username == user.Username);

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
                verifyPasswordHash(user.Password, existingUser.PasswordHash, existingUser.PasswordSalt);
            if (!verifyPassword)
            {
                return new AuthResponseDto
                {
                    Message = "Wrong Password",
                    Token = string.Empty,
                    Success = false
                };
            }

            string token = createToken(existingUser);
             RefreshToken refreshtoken = createRefreshToken();
            setRefreshToken(refreshtoken, existingUser);
            return new AuthResponseDto
            {
                Message = "User logged in successfully",
                Token = token,
                Success = true,
                RefreshToken = refreshtoken.Token,
                TokenExpires = refreshtoken.Expires
            };

        }

        public async Task<AuthResponseDto> Logout()
        {
            
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("refreshToken");
            
            return new AuthResponseDto
            {
                Message = "User logged out successfully",
                Success = true
            };
        }
        public async Task<AuthResponseDto> RefreshToken()
        {
            string refreshToken = _httpContextAccessor?.HttpContext?.Request.Cookies["refreshToken"];
            Users user = await _repository.GetByNameAsync(x => x.RefreshToken == refreshToken);

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

            string token = createToken(user);
            RefreshToken newRefreshToken = createRefreshToken();
            setRefreshToken(newRefreshToken, user);

            return new AuthResponseDto()
            {
                Success = true,
                Token = token,
                RefreshToken = newRefreshToken.Token,
                TokenExpires = newRefreshToken.Expires
            };
        }

        private void createPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (HMACSHA512 hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool verifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (HMACSHA512 hmac = new HMACSHA512(passwordSalt))
            {
                byte[] computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string createToken(Users user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Authentication:Secret_key").Value));

            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            JwtSecurityToken token = new JwtSecurityToken(
                claims: claims,
                expires:DateTime.Now.AddDays(7),
                signingCredentials: credentials
                );

            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);  
            return jwtToken;

        }

        private RefreshToken createRefreshToken()
        {
            RefreshToken refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now
            };

            return refreshToken;
        }

        private async Task setRefreshToken(RefreshToken refreshToken, Users user)
        {
            CookieOptions cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = refreshToken.Expires
            };

            _httpContextAccessor?.HttpContext?.Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);
            user.RefreshToken = refreshToken.Token;
            user.TokenCreated = refreshToken.Created;
            user.TokenExpires = refreshToken.Expires;
            await _repository.SaveChangesAsync();
        }
    }
}
