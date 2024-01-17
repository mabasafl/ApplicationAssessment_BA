using Application.Core.Helpers.Interfaces;
using Application.Core.Interfaces;
using Application.DataTransfer.Dtos.Core;
using Application.Data.Models.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.API.Controllers
{
    public class BusinessAreaTypeController : DirectoryController<BusinessAreaType, BusinessAreaTypeDto>
    {

        public BusinessAreaTypeController(IDirectoryService<BusinessAreaType, BusinessAreaTypeDto> directoryService, IValidationHelper<BusinessAreaType> validationHelper) : base(directoryService, validationHelper)
        {
            
        }
    }
}
