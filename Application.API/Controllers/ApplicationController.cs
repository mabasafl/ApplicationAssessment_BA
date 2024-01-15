using System.Linq.Expressions;
using Application.DataTransfer.Dtos.Core;
using Application.Data.Models.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Core.Interfaces;

namespace Application.API.Controllers
{
    public class ApplicationController : DirectoryController<Applications, ApplicationsDto>
    {
        private IDirectoryService<Applications, ApplicationsDto> _directoryService;

        public ApplicationController(IDirectoryService<Applications, ApplicationsDto> directoryService) : base(
            directoryService)
        {
            _directoryService = directoryService;

        }


        [HttpGet("getByName")]
        public async Task<IActionResult> GetByNameAsync(string name)
        {
            Expression<Func<Applications, bool>> predicate = x => x.Name == name;

            ApplicationsDto response = await _directoryService.GetDirectoryByNameAsync(predicate);
            return Ok(response);
        }
    }
}
