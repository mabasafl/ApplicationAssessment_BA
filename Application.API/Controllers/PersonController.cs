using Application.Core.Helpers.Interfaces;
using Application.Core.Interfaces;
using Application.DataTransfer.Dtos.Core;
using Application.Data.Models.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.API.Controllers
{
    public class PersonController : DirectoryController<Person, PersonDto>
    {
        public PersonController(IDirectoryService<Person, PersonDto> directoryService): base(directoryService)
        {
            
        }
    }
}
