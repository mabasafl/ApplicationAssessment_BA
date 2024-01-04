using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Data.Dtos.Core;

namespace Application.Core.Helpers.Interfaces
{
    public interface IValidationService<T> where T: class
    {
        Task<ResponseDto> Unique(string name);
    }
}
