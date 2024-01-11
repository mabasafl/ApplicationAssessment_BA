using Application.Core.Interfaces;
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
        public async Task<IActionResult> GetAllBusinessAreaFiltering(int businessAreaId)
        {
            var response = await _businessAreaFiltering.GetAllBusinessAreaRelationshipsAsync(businessAreaId);
            return Ok(response);
        }

        [HttpGet("getAllData")]
        public async Task<IActionResult> GetAllDataBusinessAreaFiltering()
        {
            var response = await _businessAreaFiltering.GetAllDataBusinessAreaRelationshipsAsync();
            return Ok(response);
        }
    }
}
