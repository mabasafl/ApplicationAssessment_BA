using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Core.DTOs;
using Application.Core.Helpers;
using Application.Core.Helpers.Interfaces;
using Application.Core.Interfaces;
using Application.Core.Models;
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
            //var result = await _repository.GetAllAsync();
            //return result;
            List<ApplicationsDto> applicationDto = new List<ApplicationsDto>();
            var result = await _repository.GetAllAsync();
            if (result != null)
            {
                //foreach (var appl in result)
                //{
                //    applicationDto.Add(
                //        new ApplicationsDto()
                //        {
                //            Name = appl.Name,
                //            DateCreated = appl.DateCreated,
                //            CreatedBy = appl.CreatedBy,
                //            DateModified = appl.DateModified,
                //            ModifiedBy = appl.ModifiedBy

                //        });
                //}

                applicationDto = _mapper.Map<List<Applications>, List<ApplicationsDto>>(result);
            }
            return applicationDto;


        }

        public async Task<ResponseDto> AddApplicationAsync(ApplicationsDto applications)
        {
            ResponseDto response = null;
            ApplicationsDto applicationDto = new ApplicationsDto();

            Applications appl = _mapper.Map<ApplicationsDto, Applications>(applications);


            var result = await _repository.AddAsync(appl);

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

        public async Task<ResponseDto> UpdateApplicationAsync(Applications application)
        {
            ResponseDto response = null;
            var result = await _repository.UpdateAsync(application);

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

        public async Task<ResponseDto> DeleteApplicationAsync(Applications application)
        {
            ResponseDto response = null;
            var result = await _repository.DeleteAsync(application);
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

        public async Task<ApplicationsDto> GetApplicationsAsync(int id)
        {
            ApplicationsDto applicationDto = new ApplicationsDto();
            var result = await _repository.GetAsync(id);

            if (result != null)
            {
                applicationDto = _mapper.Map<Applications, ApplicationsDto>(result);
            }
            return applicationDto;
        }
    }
}
