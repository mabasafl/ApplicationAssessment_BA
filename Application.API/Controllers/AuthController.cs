
using Application.Authentication.Interface;
using Application.Data.Dtos.Auth;
using Application.Data.Models;
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
            var response = await _authService.RegisterUser(user);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<Users>> Login(UserDto user)
        {
            var response = await _authService.Login(user);
            if (response.Success)
            {
                return Ok(response);
            }

            return Unauthorized(response.Message);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<string>> RefreshToken()
        {
            var response = await _authService.RefreshToken();
            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response.Message);
        }

    }
}
