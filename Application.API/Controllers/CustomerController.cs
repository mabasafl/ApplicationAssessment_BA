using Application.Core.Helpers.Interfaces;
using Application.Core.Interfaces;
using Application.Data.Dtos.Core;
using Application.Data.Models.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IValidationService<Customer> _validationService;

        public CustomerController(ICustomerService customerService, IValidationService<Customer> validationService)
        {
            _customerService = customerService;
            _validationService = validationService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _customerService.GetAllCustomersAsync();
            return Ok(response);
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _customerService.GetCustomerAsync(id);
            return Ok(response);
        }

        [HttpPost("post")]
        public async Task<IActionResult> Post(CustomersDto customer)
        {
            ResponseDto isCustomerValid = await _validationService.Unique(customer.Name);

            if (isCustomerValid.Message.Any()) return BadRequest(isCustomerValid);

            var response = await _customerService.AddCustomerAsync(customer);
            return Ok(response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Put(Customer customer)
        {
            var response = await _customerService.UpdateCustomerAsync(customer);
            return Ok(response);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(Customer customer)
        {
            var response = await _customerService.DeleteCustomerAsync(customer);
            return Ok(response);
        }
    }
}
