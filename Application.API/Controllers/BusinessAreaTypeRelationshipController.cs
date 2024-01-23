using Application.Core.Interfaces;
using Application.Data.Models.Core;
using Application.DataTransfer.Dtos.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessAreaTypeRelationshipController : ControllerBase
    {
        private readonly IBusinessAreaTypeRelationshipService _businessAreaTypeRelationshipService;
        public BusinessAreaTypeRelationshipController(IBusinessAreaTypeRelationshipService businessAreaTypeRelationshipService)
        {
            _businessAreaTypeRelationshipService = businessAreaTypeRelationshipService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllBusinessAreaTypeRelations(int customerId)
        {
            List<BusinessAreaTypeRelationshipDto> response =
                await _businessAreaTypeRelationshipService.GetAllBusinessAreaTypeRelationshipAsync(customerId);
            return Ok(response);
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetBusinessAreaTypeRelations(int businessAreaId)
        {
            BusinessAreaTypeRelationshipDto response =
                await _businessAreaTypeRelationshipService.GetBusinessAreaTypeRelationshipAsync(businessAreaId);
            return Ok(response);
        }

        [HttpPost("post")]
        public async Task<IActionResult> AddBusinessAreaTypeRelationship(BusinessAreaTypeRelationshipDto data)
        {
            ResponseDto response = await _businessAreaTypeRelationshipService.AddBusinessAreaTypeRelationshipAsync(data);
            
            return Ok(response);
        }

        [HttpPut("put")]
        public async Task<IActionResult> UpdateBusinessAreTypeRelationship(BusinessAreaTypeRelationship data)
        {
            ResponseDto response =
                await _businessAreaTypeRelationshipService.UpdateBusinessAreaTypeRelationshipAsync(data);
            return Ok(response);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteBusinessAreTypeRelationship(BusinessAreaTypeRelationshipDto data)
        {
            ResponseDto response =
                await _businessAreaTypeRelationshipService.DeleteBusinessAreaTypeRelationshipAsync(data);
            return Ok(response);
        }
    }
}
