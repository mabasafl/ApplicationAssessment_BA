﻿using Application.DataTransfer.Dtos.Core;
using Application.Core.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Helpers
{
    public class ResponseMessageHelper : IResponseMessageHelper
    {
        public async Task<ResponseDto> ResponseMessage(bool success, List<string[]> message)
        {
            ResponseDto responseBody = new ResponseDto
            {
                Success = success,
                Message = message,
                TimeStamp = DateTime.Now
            };

            return await Task.FromResult(responseBody);
        }
    }
}
