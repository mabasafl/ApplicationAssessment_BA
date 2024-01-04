using Application.Data.Dtos.Core;
using Application.Data.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Interfaces
{
    public interface ICustomerService
    {
        Task<ResponseDto> AddCustomerAsync(ApplicationsDto application);
        Task<ResponseDto> UpdateCustomerAsync(Applications application);
        Task<List<ApplicationsDto>> GetAllCustomersAsync();
        Task<ApplicationsDto> GetCustomerAsync(int id);
        Task<ResponseDto> DeleteCustomerAsync(Applications application);
    }
}
