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
        Task<ResponseDto> AddCustomerAsync(CustomersDto customer);
        Task<ResponseDto> UpdateCustomerAsync(Customer customer);
        Task<List<CustomersDto>> GetAllCustomersAsync();
        Task<CustomersDto> GetCustomerAsync(int id);
        Task<ResponseDto> DeleteCustomerAsync(Customer customer);
    }
}
