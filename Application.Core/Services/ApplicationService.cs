using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Data.Dtos.Core;
using Application.Core.Helpers;
using Application.Core.Helpers.Interfaces;
using Application.Core.Interfaces;
using Application.Data.Models.Core;
using Application.Core.Repositories;
using Application.Core.Repositories.Interfaces;
using AutoMapper;

namespace Application.Core.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IBaseRepository<Applications> _repository;
        private readonly IResponseMessageService _responseMessageService;
        private readonly IMapper _mapper;

        public ApplicationService(IBaseRepository<Applications> repository, IMapper mapper)
        {
            _repository = repository;
            _responseMessageService = new ResponseMessageService();
            _mapper = mapper;
        }
        public async Task<List<ApplicationsDto>> GetAllApplicationsAsync()
        {
            List<ApplicationsDto> application = new List<ApplicationsDto>();
            var result = await _repository.GetAllAsync();
            if (result != null)
            {
                application = _mapper.Map<List<Applications>, List<ApplicationsDto>>(result);
            }
            return application;


        }
        public async Task<ApplicationsDto> GetApplicationsAsync(int id)
        {
            ApplicationsDto application = new ApplicationsDto();
            var result = await _repository.GetAsync(id);

            if (result != null)
            {
                application = _mapper.Map<Applications, ApplicationsDto>(result);
            }
            return application;
        }
        public async Task<ResponseDto> AddApplicationAsync(ApplicationsDto applicationRequest)
        {
            ResponseDto response = null;
            //ApplicationsDto applicationDto = new ApplicationsDto();

            Applications application = _mapper.Map<ApplicationsDto, Applications>(applicationRequest);


            var result = await _repository.AddAsync(application);

            if (result)
            {
                response =await _responseMessageService.ResponseMessage(result,
                    new List<string[]> { new[] { "adding application was successful", "Thank You" } });
            }
            else
            {
                response = await _responseMessageService.ResponseMessage(result,
                    new List<string[]>{new[] { "adding application was not successful", "Please try again later" }});
            }

            return response;
        }
        public async Task<ResponseDto> UpdateApplicationAsync(Applications applicationRequest)
        {
            ResponseDto response = null;
            var result = await _repository.UpdateAsync(applicationRequest);

            if (result)
            {
                response = await _responseMessageService.ResponseMessage(result, new List<string[]> { new[] { "updating application was successful", "Thank you!" } });
            }
            else
            {
                response = await _responseMessageService.ResponseMessage(result,
                    new List<string[]> { new[] { "updating application was not successful", "Please try again later" } });
            }

            return response;
        }
        public async Task<ResponseDto> DeleteApplicationAsync(Applications applicationRequest)
        {
            ResponseDto response = null;
            var result = await _repository.DeleteAsync(applicationRequest);
            if (result)
            {
                response = await _responseMessageService.ResponseMessage(result, new List<string[]>{new[] { "deleting application was successful", "Thank you!" }});
            }
            else
            {
                response = await _responseMessageService.ResponseMessage(result,
                    new List<string[]> { new[] { "deleting application was not successful", "Please try again later" } });
            }

            return response;
        }


    }
}
