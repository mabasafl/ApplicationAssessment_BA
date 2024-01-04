﻿using Application.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Helpers.Interfaces
{
    public interface IResponseMessageService
    {
        Task<ResponseDto> ResponseMessage(bool success, List<string[]> message);
    }
}
