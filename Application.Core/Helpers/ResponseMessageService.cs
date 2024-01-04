using Application.Core.DTOs;
using Application.Core.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Helpers
{
    public class ResponseMessageService : IResponseMessageService
    {
        public async Task<ResponseDto> ResponseMessage(bool success, List<string[]> message)
        {
            var responseBody = new ResponseDto
            {
                Success = success,
                Message = message,
                TimeStamp = DateTime.Now
            };

            return await Task.FromResult(responseBody);
        }
    }
}
