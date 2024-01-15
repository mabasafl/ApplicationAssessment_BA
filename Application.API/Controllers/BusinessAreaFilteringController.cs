using Application.Core.Interfaces;
using Application.Data.Models.Core;
using Application.DataTransfer.Dtos.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessAreaFilteringController : ControllerBase
    {
        private readonly IBusinessAreaFilteringService _businessAreaFiltering;
        public BusinessAreaFilteringController(IBusinessAreaFilteringService businessAreaFiltering)
        {
            _businessAreaFiltering = businessAreaFiltering;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllBusinessAreaFiltering(int businessAreaId, int customerId)
        {
            List<BusinessAreaRelationshipDto> response = await _businessAreaFiltering.GetAllBusinessAreaRelationshipsAsync(businessAreaId, customerId);
            return Ok(response);
        }

        [HttpGet("getAllData")]
        public async Task<IActionResult> GetAllDataBusinessAreaFiltering()
        {
            List<BusinessAreaRelationshipDto> response = await _businessAreaFiltering.GetAllDataBusinessAreaRelationshipsAsync();
            return Ok(response);
        }

        [HttpGet("getAllCascade")]
        public async Task<IActionResult> GetAllCascadeFiltering(int businessArea1, int businessArea2, int businessArea3, int customerId, int applicationId)
        {
            List<PersonDto> response =
                await _businessAreaFiltering.GetCascadeFiltering(businessArea1, businessArea2, businessArea3, customerId, applicationId);
            return Ok(response);
        }

        [HttpGet("getDropdown")]
        public async Task<IActionResult> GetDropDown(int businessArea1, int businessArea2, int customerId, int applicationId)
        {
            List<BusinessAreaRelationshipDto> response =
                await _businessAreaFiltering.GetDropDownAsync(businessArea1, businessArea2, customerId, applicationId);
            return Ok(response);
        }
    }
}
