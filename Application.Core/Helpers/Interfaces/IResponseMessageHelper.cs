using Application.DataTransfer.Dtos.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Helpers.Interfaces
{
    public interface IResponseMessageHelper
    {
        Task<ResponseDto> ResponseMessage(bool success, List<string[]> message);
    }
}
