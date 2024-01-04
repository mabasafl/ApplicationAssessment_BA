using Application.Core.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectoryController<T> : ControllerBase where T : class
    {
        private readonly IBaseRepository<T> _baseRepository;

        public DirectoryController(IBaseRepository<T> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        [HttpGet("getAll")]
        //public List<T> GetAll()
        public async Task<IActionResult> GetAll()
        {
            var response = _baseRepository.GetAllAsync();
            return Ok(response);
        }

        [HttpGet("get")]
        //public T Get(int id)
        public async Task<IActionResult> Get(int id)
        {
            var response = _baseRepository.GetAsync(id);
            return Ok(response);
        }

        [HttpPost("post")]
        //public bool Post(T entity)
        public async Task<IActionResult> Post(T entity)
        {
            var response = _baseRepository.AddAsync(entity);
            return Ok(response);
        }

        [HttpPut("update")]
        //public bool Put(T entity)
        public async Task<IActionResult> Put(T entity)
        {
            var response = _baseRepository.UpdateAsync(entity);
            return Ok(response);
        }

        [HttpDelete("delete")]
        //public bool Delete(T entity)
        public async Task<IActionResult> Delete(T entity)
        {
            var response = _baseRepository.DeleteAsync(entity);
            return Ok(response);
        }

    }
}
