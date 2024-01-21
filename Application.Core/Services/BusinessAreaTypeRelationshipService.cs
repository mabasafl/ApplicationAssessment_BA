using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Application.Core.Helpers;
using Application.Core.Helpers.Interfaces;
using Application.Core.Interfaces;
using Application.Core.Repositories.Interfaces;
using Application.Data.Models.Core;
using Application.DataTransfer.Dtos.Core;
using AutoMapper;

namespace Application.Core.Services
{
    public class BusinessAreaTypeRelationshipService : IBusinessAreaTypeRelationshipService
    {
        private readonly IBaseRepository<BusinessAreaTypeRelationship> _repository;
        private readonly IMapper _mapper;
        private readonly IDirectoryService<Customer, CustomersDto> _customerService;
        private readonly IDirectoryService<BusinessArea, BusinessAreaDto> _businessAreaService;
        private readonly INewInstanceHelper _instanceHelper;
        private readonly IResponseMessageHelper _responseMessageHelper;

        public BusinessAreaTypeRelationshipService(IBaseRepository<BusinessAreaTypeRelationship> repository, IMapper mapper, INewInstanceHelper instanceHelper, IBaseRepository<Customer> customerRepository, IBaseRepository<BusinessArea> businessAreaRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _instanceHelper = instanceHelper;
            _customerService = new DirectoryService<Customer, CustomersDto>(customerRepository, _mapper, _instanceHelper, null);
            _businessAreaService =
                new DirectoryService<BusinessArea, BusinessAreaDto>(businessAreaRepository, _mapper, _instanceHelper,
                    null);
            _responseMessageHelper = new ResponseMessageHelper();
        }
        public async Task<ResponseDto> AddBusinessAreaTypeRelationshipAsync(BusinessAreaTypeRelationshipDto data)
        {
            ResponseDto response = new ResponseDto();

            try
            {
                BusinessAreaTypeRelationship businessAreaTypeRelationship =
                    _mapper.Map<BusinessAreaTypeRelationshipDto, BusinessAreaTypeRelationship>(data);

                List<BusinessAreaTypeRelationship> exist = await _repository.GetAllAsync();
                exist = exist.Where(x => x.CustomerId == data.CustomerId
                                         && x.BusinessAreaId == data.BusinessAreaId
                                         && x.BusinessAreaType1 == data.BusinessAreaType1
                                         && x.BusinessAreaType2 == data.BusinessAreaType2
                                         && x.BusinessAreaType3 == data.BusinessAreaType3).ToList();

               
                if (exist.Any())
                    return response = await _responseMessageHelper.ResponseMessage(false,
                        new List<string[]>
                        {
                            new[]
                            {
                                "adding business area type relationship was not successful",
                                "Relationship already exist for this customer"
                            }
                        });

                bool result = await _repository.AddAsync(businessAreaTypeRelationship);

                if (!result)
                {
                    response = await _responseMessageHelper.ResponseMessage(result,
                        new List<string[]> { new[] { "adding business area type relationship was not successful", "Please try again later" } });
                }

                response = await _responseMessageHelper.ResponseMessage(result,
                    new List<string[]> { new[] { "adding business area type relationship was successful", "Thank You" } });

                return response;
            }
            catch (Exception e)
            {
                return response;
            }
        }

        public async Task<ResponseDto> DeleteBusinessAreaTypeRelationshipAsync(BusinessAreaTypeRelationship entity)
        {
            ResponseDto response = new ResponseDto();

            try
            {
                bool result = await _repository.DeleteAsync(entity);

                if (!result)
                {
                    response = await _responseMessageHelper.ResponseMessage(result,
                        new List<string[]> { new[] { "deleting business area type relationship was not successful", "Please try again later" } });
                }

                response = await _responseMessageHelper.ResponseMessage(result,
                    new List<string[]> { new[] { "deleting business area type relationship was successful", "Thank You" } });

                return response;
            }
            catch (Exception e)
            {
                return response;
            }
        }

        public async Task<List<BusinessAreaTypeRelationshipDto>> GetAllBusinessAreaTypeRelationshipAsync(int customerId)
        {
            List<BusinessAreaTypeRelationshipDto> businessAreaTypeRelationships = new List<BusinessAreaTypeRelationshipDto>();
            try
            {
                List<BusinessAreaTypeRelationship> result = await _repository.GetAllAsync();

                if (!result.Any()) return businessAreaTypeRelationships;

                List<BusinessAreaTypeRelationshipDto> businessAreaTypeRelationship =
                    _mapper.Map<List<BusinessAreaTypeRelationship>, List<BusinessAreaTypeRelationshipDto>>(result);

                List<BusinessAreaTypeRelationshipDto> filtersBusinessAreaTypeRelationship =
                    businessAreaTypeRelationship
                        .Where(x => x.CustomerId == customerId && x.IsActive == true).ToList();

                CustomersDto customer = new CustomersDto();

                foreach (var element in filtersBusinessAreaTypeRelationship)
                {
                    customer = await _customerService.GetDirectoryAsync(element.CustomerId);
                    element.CustomerName = customer.Name;



                    BusinessAreaDto businessArea = await _businessAreaService.GetDirectoryAsync(element.BusinessAreaId);
                    element.BusinessAreaName = businessArea.Name;

                    businessAreaTypeRelationships.Add(element);
                }

                return businessAreaTypeRelationships;
            }
            catch (Exception e)
            {
                return businessAreaTypeRelationships;
            }
        }

        public async Task<BusinessAreaTypeRelationshipDto> GetBusinessAreaTypeRelationshipAsync(int id)
        {
            BusinessAreaTypeRelationshipDto buisnessAreaTypeRelationship = new BusinessAreaTypeRelationshipDto();
            try
            {
                List<BusinessAreaTypeRelationshipDto> allBusinessAReaTypeRelationships =
                    await GetAllBusinessAreaTypeRelationshipAsync(1);
                buisnessAreaTypeRelationship =
                    allBusinessAReaTypeRelationships.Where(x => x.BusinessAreaId == id)?.FirstOrDefault();

                if (buisnessAreaTypeRelationship == null)
                {
                    buisnessAreaTypeRelationship = new BusinessAreaTypeRelationshipDto();
                    buisnessAreaTypeRelationship.BusinessAreaType1 = true;
                    buisnessAreaTypeRelationship.BusinessAreaType2 = true;
                    buisnessAreaTypeRelationship.BusinessAreaType3 = true;
                }


                return buisnessAreaTypeRelationship;
            }
            catch (Exception e)
            {
                return buisnessAreaTypeRelationship;
            }
        }

        public async Task<ResponseDto> UpdateBusinessAreaTypeRelationshipAsync(BusinessAreaTypeRelationship entity)
        {
            ResponseDto response = new ResponseDto();

            try
            {
                bool result = await _repository.UpdateAsync(entity);

                if (!result)
                {
                    response = await _responseMessageHelper.ResponseMessage(result,
                        new List<string[]> { new[] { "updating business area type relationship was not successful", "Please try again later" } });
                }

                response = await _responseMessageHelper.ResponseMessage(result,
                    new List<string[]> { new[] { "updating business area type relationship was successful", "Thank You" } });

                return response;
            }
            catch (Exception e)
            {
                return response;
            }
        }
    }
}
