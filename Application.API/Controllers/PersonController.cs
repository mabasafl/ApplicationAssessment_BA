using Application.Core.Helpers.Interfaces;
using Application.Core.Interfaces;
using Application.Data.Dtos.Core;
using Application.Data.Models.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly IValidationService<Person> _validationService;

        public PersonController(IPersonService personService, IValidationService<Person> validationService)
        {
            _personService = personService;
            _validationService = validationService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _personService.GetAllPersonAsync();
            return Ok(response);
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _personService.GetPersonAsync(id);
            return Ok(response);
        }

        [HttpPost("post")]
        public async Task<IActionResult> Post(PersonDto person)
        {
            ResponseDto isPersonEmailValid = await _validationService.Unique(person.EmailAddress);

            if (isPersonEmailValid.Message.Any()) return BadRequest(isPersonEmailValid);

            var response = await _personService.AddPersonAsync(person);
            return Ok(response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Put(Person person)
        {
            var response = await _personService.UpdatePersonAsync(person);
            return Ok(response);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(Person person)
        {
            var response = await _personService.DeletePersonAsync(person);
            return Ok(response);
        }
    }
}
