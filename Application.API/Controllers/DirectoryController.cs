using System.Linq.Expressions;
using Application.Core.Helpers;
using Application.Core.Helpers.Interfaces;
using Application.Core.Interfaces;
using Application.DataTransfer.Dtos.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectoryController<Entity,Dto> : ControllerBase where Entity : class
    {
        private readonly IDirectoryService<Entity,Dto> _directoryService;
        private readonly IValidationHelper<Entity> _validationHelper;
        private readonly INewInstanceHelper _instanceHelper;

        public DirectoryController(IDirectoryService<Entity,Dto> directoryService, IValidationHelper<Entity> validationHelper)
        {
            _directoryService = directoryService;
            _validationHelper = validationHelper;
            _instanceHelper = new NewInstanceHelper();
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            List<Dto> response = await _directoryService.GetAllDirectoryAsync();
            return Ok(response);
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetAsync(int id)
        {
            Dto response = await _directoryService.GetDirectoryAsync(id);
            return Ok(response);
        }

        [HttpPost("post")]
        public async Task<IActionResult> PostAsync(Dto entity)
        {
            ResponseDto response = await _directoryService.AddDirectoryAsync(entity);
            if (!response.Success) return BadRequest(response);
            return Ok(response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateAsync(Entity entity)
        {
            ResponseDto response = await _directoryService.UpdateDirectoryAsync(entity);
            return Ok(response);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteAsync(Entity entity)
        {
            ResponseDto response = await _directoryService.DeleteDirectoryAsync(entity);
            return Ok(response);
        }

    }
}
