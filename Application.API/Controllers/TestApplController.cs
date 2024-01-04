using Application.Data.Models.Core;
using Application.Core.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestApplController : DirectoryController<Applications>
    {
        public TestApplController(IBaseRepository<Applications> baseRepository) : base(baseRepository)
        {
            
        }
    }

}
