using Application.Core.Helpers.Interfaces;
using Application.Core.Interfaces;
using Application.DataTransfer.Dtos.Core;
using Application.Data.Models.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.API.Controllers
{
    public class CustomerController : DirectoryController<Customer, CustomersDto>
    {
        public CustomerController(IDirectoryService<Customer, CustomersDto> directoryService, IValidationHelper<Customer> validationHelper) : base(directoryService, validationHelper)
        {
            
        }
    }
}
