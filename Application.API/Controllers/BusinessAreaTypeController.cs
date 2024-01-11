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
    public class BusinessAreaTypeController : ControllerBase
    {
        private readonly IBusinessAreaTypeService _businessAreaTypeService;
        private readonly IValidationService<Customer> _validationService;

        public BusinessAreaTypeController(IBusinessAreaTypeService businessAreaTypeService, IValidationService<Customer> validationService)
        {
            _businessAreaTypeService = businessAreaTypeService;
            _validationService = validationService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _businessAreaTypeService.GetAllBusinessAreaTypesAsync();
            return Ok(response);
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _businessAreaTypeService.GetBusinessAreaTypeAsync(id);
            return Ok(response);
        }

        [HttpPost("post")]
        public async Task<IActionResult> Post(BusinessAreaTypeDto businessAreaType)
        {
            ResponseDto isBusinessAreaTypeValid = await _validationService.Unique(businessAreaType.Name);

            if (isBusinessAreaTypeValid.Message.Any()) return BadRequest(isBusinessAreaTypeValid);

            var response = await _businessAreaTypeService.AddBusinessAreaTypeAsync(businessAreaType);
            return Ok(response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Put(BusinessAreaType businessAreaType)
        {
            var response = await _businessAreaTypeService.UpdateBusinessAreaTypeAsync(businessAreaType);
            return Ok(response);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(BusinessAreaType businessAreaType)
        {
            var response = await _businessAreaTypeService.DeleteBusinessAreaTypeAsync(businessAreaType);
            return Ok(response);
        }
    }
}
