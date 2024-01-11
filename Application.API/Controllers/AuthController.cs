
using Application.Authentication.Interface;
using Application.Data.Models.Auth;
using Application.DataTransfer.Dtos.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("register")]
        public async Task<ActionResult<Users>> RegisterUser(UserDto user)
        {
            Users response = await _authService.RegisterUser(user);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<Users>> Login(UserDto user)
        {
            AuthResponseDto response = await _authService.Login(user);
            if (response.Success)
            {
                return Ok(response);
            }

            return Unauthorized(response.Message);
        }

        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            AuthResponseDto response = await _authService.Logout();
            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest("User not logged out");
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<string>> RefreshToken()
        {
            AuthResponseDto response = await _authService.RefreshToken();
            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response.Message);
        }

    }
}
