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
    public class BusinessAreaController : ControllerBase
    {
        private readonly IBusinessAreaService _businessAreaService;
        private readonly IValidationService<Customer> _validationService;

        public BusinessAreaController(IBusinessAreaService businessAreaService, IValidationService<Customer> validationService)
        {
            _businessAreaService = businessAreaService;
            _validationService = validationService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _businessAreaService.GetAllBusinessAreasAsync();
            return Ok(response);
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _businessAreaService.GetBusinessAreaAsync(id);
            return Ok(response);
        }

        [HttpPost("post")]
        public async Task<IActionResult> Post(BusinessAreaDto businessArea)
        {
            ResponseDto isBusinessAreaValid = await _validationService.Unique(businessArea.Name);

            if (isBusinessAreaValid.Message.Any()) return BadRequest(isBusinessAreaValid);

            var response = await _businessAreaService.AddBusinessAreaAsync(businessArea);
            return Ok(response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Put(BusinessArea businessArea)
        {
            var response = await _businessAreaService.UpdateBusinessAreaAsync(businessArea);
            return Ok(response);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(BusinessArea businessArea)
        {
            var response = await _businessAreaService.DeleteBusinessAreaAsync(businessArea);
            return Ok(response);
        }
    }
}
