﻿

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Data.Models.Auth;
using Application.DataTransfer.Dtos.Auth;

namespace Application.Authentication.Interface
{
    public interface IAuthService
    {
        Task<Users> RegisterUser(UserDto user);
        Task<AuthResponseDto> Login(UserDto user);
        Task<AuthResponseDto> RefreshToken();
        Task<AuthResponseDto> Logout();

    }
}
