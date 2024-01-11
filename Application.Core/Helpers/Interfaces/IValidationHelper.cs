using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DataTransfer.Dtos.Core;

namespace Application.Core.Helpers.Interfaces
{
    public interface IValidationHelper<Entity> where Entity: class
    {
        Task<ResponseDto> Unique(string name);
    }
}
