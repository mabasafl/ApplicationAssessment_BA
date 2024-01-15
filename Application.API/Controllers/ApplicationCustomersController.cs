using Application.Core.Interfaces;
using Application.Core.Services;
using Application.DataTransfer.Dtos.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationCustomersController : ControllerBase
    {
        private readonly IApplicationCustomerService _applicationCustomersService;
        public ApplicationCustomersController(IApplicationCustomerService applicationCustomersService)
        {
            _applicationCustomersService = applicationCustomersService;
        }

        [HttpGet("{friendlyUrl}")]
        public async Task<ActionResult> GetApplicationCustomers(string friendlyUrl)
        {
            List<ApplicationCustomerDto> response = await _applicationCustomersService.GetApplicationByFriendlyUrl(friendlyUrl);
            return Ok(response);
        }
    }
}
