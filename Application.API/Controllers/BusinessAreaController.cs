using Application.Core.Helpers.Interfaces;
using Application.Core.Interfaces;
using Application.Core.Repositories;
using Application.DataTransfer.Dtos.Core;
using Application.Data.Models.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.API.Controllers
{
    public class BusinessAreaController : DirectoryController<BusinessArea, BusinessAreaDto>
    {
        public BusinessAreaController(IDirectoryService<BusinessArea, BusinessAreaDto> directoryService, IValidationHelper<BusinessArea> validationHelper) : base(directoryService, validationHelper)
        {
            
        }
    }
}
