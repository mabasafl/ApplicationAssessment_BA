using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Core.Helpers.Interfaces;
using Application.Core.Helpers;
using Application.Core.Interfaces;
using Application.Core.Repositories.Interfaces;
using Application.Data.Dtos.Core;
using Application.Data.Models.Core;
using AutoMapper;

namespace Application.Core.Services
{
    public class PersonService : IPersonService
    {
        private readonly IBaseRepository<Person> _repository;
        private readonly IResponseMessageService _responseMessageService;
        private readonly IMapper _mapper;

        public PersonService(IBaseRepository<Person> repository, IMapper mapper)
        {
            _repository = repository;
            _responseMessageService = new ResponseMessageService();
            _mapper = mapper;
        }
        public async Task<List<PersonDto>> GetAllPersonAsync()
        {
            List<PersonDto> person = new List<PersonDto>();
            var result = await _repository.GetAllAsync();
            if (result != null)
            { person = _mapper.Map<List<Person>, List<PersonDto>>(result);
            }
            return person;
        }

        public async Task<PersonDto> GetPersonAsync(int id)
        {
            PersonDto person = new PersonDto();
            var result = await _repository.GetAsync(id);

            if (result != null)
            {
                person = _mapper.Map<Person, PersonDto>(result);
            }
            return person;
        }
        public async Task<ResponseDto> AddPersonAsync(PersonDto personRequest)
        {
            ResponseDto response = null;
            //PersonDto personDto = new PersonDto();

            Person person = _mapper.Map<PersonDto, Person>(personRequest);
            //personMapping.AlternateId = Guid.NewGuid().ToString();


            var result = await _repository.AddAsync(person);

            if (result)
            {
                response = await _responseMessageService.ResponseMessage(result,
                    new List<string[]> { new[] { "adding person was successful", "Thank You" } });
            }
            else
            {
                response = await _responseMessageService.ResponseMessage(result,
                    new List<string[]> { new[] { "adding person was not successful", "Please try again later" } });
            }

            return response;
        }

        public async Task<ResponseDto> DeletePersonAsync(Person personRequest)
        {
            ResponseDto response = null;
            var result = await _repository.DeleteAsync(personRequest);
            if (result)
            {
                response = await _responseMessageService.ResponseMessage(result, new List<string[]> { new[] { "deleting person was successful", "Thank you!" } });
            }
            else
            {
                response = await _responseMessageService.ResponseMessage(result,
                    new List<string[]> { new[] { "deleting person was not successful", "Please try again later" } });
            }

            return response;
        }

        public async Task<ResponseDto> UpdatePersonAsync(Person personRequest)
        {
            ResponseDto response = null;
            var result = await _repository.UpdateAsync(personRequest);

            if (result)
            {
                response = await _responseMessageService.ResponseMessage(result, new List<string[]> { new[] { "updating person was successful", "Thank you!" } });
            }
            else
            {
                response = await _responseMessageService.ResponseMessage(result,
                    new List<string[]> { new[] { "updating person was not successful", "Please try again later" } });
            }

            return response;
        }
    }
}
