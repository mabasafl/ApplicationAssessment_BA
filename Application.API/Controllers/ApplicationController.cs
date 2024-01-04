using Application.Core.DTOs;
using Application.Core.Helpers.Interfaces;
using Application.Core.Interfaces;
using Application.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _applicationService;
        private readonly IValidationService<Applications> _validationService;

        public ApplicationController(IApplicationService applicationService, IValidationService<Applications> validationService)
        {
            _applicationService = applicationService;
            _validationService = validationService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _applicationService.GetAllApplicationsAsync();
            return Ok(response);
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _applicationService.GetApplicationsAsync(id);
            return Ok(response);
        }

        [HttpPost("post")] 
        public async Task<IActionResult> Post(ApplicationsDto application)
        {
            ResponseDto isApplicationValid = await _validationService.Unique(application.Name);

            if (isApplicationValid.Message.Any()) return BadRequest(isApplicationValid);

            var response = await _applicationService.AddApplicationAsync(application);
            return Ok(response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Put(Applications application)
        {
            var response = await _applicationService.UpdateApplicationAsync(application);
            return Ok(response);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(Applications application)
        {
            var response = await _applicationService.DeleteApplicationAsync(application);
            return Ok(response);
        }

        
    }
}
