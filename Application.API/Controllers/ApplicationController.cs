using Application.DataTransfer.Dtos.Core;
using Application.Data.Models.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Core.Interfaces;

namespace Application.API.Controllers
{
    public class ApplicationController : DirectoryController<Applications, ApplicationsDto>
    {
        public ApplicationController(IDirectoryService<Applications,ApplicationsDto> directoryService) : base(directoryService) { }
    }
}
