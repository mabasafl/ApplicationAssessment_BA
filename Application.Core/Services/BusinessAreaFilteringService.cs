using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Core.Helpers.Interfaces;
using Application.Core.Interfaces;
using Application.Core.Repositories;
using Application.Core.Repositories.Interfaces;
using Application.DataTransfer.Dtos.Core;
using Application.Data.Models.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions.Impl;
using Application.Core.Helpers;

namespace Application.Core.Services
{
    public class BusinessAreaFilteringService : IBusinessAreaFilteringService
    {
        private readonly IBaseRepository<BusinessAreaRelationship> _repository;
        private readonly IMapper _mapper;
        private readonly IDirectoryService<Customer, CustomersDto> _customerService;
        private readonly IDirectoryService<BusinessAreaType, BusinessAreaTypeDto> _businessAreaTypeService;
        private readonly IDirectoryService<Person, PersonDto> _personService;
        private readonly IDirectoryService<ApplicationCustomer, ApplicationCustomerDto> _applicationCustomerService;
        private readonly IDirectoryService<BusinessArea, BusinessAreaDto> _businessAreaService;
        private readonly INewInstanceHelper _instanceHelper;
        private readonly IResponseMessageHelper _responseMessageHelper;
        private readonly IValidationHelper<Customer> _customerValidationHelper;
        private readonly IBusinessAreaTypeRelationshipService _businessAreaTypeRelationshipService;
        public BusinessAreaFilteringService(IBaseRepository<BusinessAreaRelationship> repository, IMapper mapper,
            INewInstanceHelper instanceHelper, IBaseRepository<Customer> customerRepository, IBaseRepository<BusinessAreaType> businessAreaTypeRepository, IBaseRepository<BusinessArea> businessAreaRepository, IDirectoryService<Person, PersonDto> personService, IDirectoryService<ApplicationCustomer, ApplicationCustomerDto> applicationCustomerService, IValidationHelper<Customer> customerValidationHelper, IBusinessAreaTypeRelationshipService businessAreaTypeRelationshipService)
        {
            _repository = repository;
            _mapper = mapper;
            _instanceHelper = instanceHelper;
            _personService = personService;
            _applicationCustomerService = applicationCustomerService;
            _responseMessageHelper = new ResponseMessageHelper();
            _customerValidationHelper = customerValidationHelper;
            _businessAreaTypeRelationshipService = businessAreaTypeRelationshipService;
            _customerService = new DirectoryService<Customer, CustomersDto>(customerRepository, _mapper, _instanceHelper, null);
            _businessAreaTypeService = new DirectoryService<BusinessAreaType, BusinessAreaTypeDto>(businessAreaTypeRepository, _mapper, _instanceHelper, null);
            _businessAreaService =
        new DirectoryService<BusinessArea, BusinessAreaDto>(businessAreaRepository, _mapper, _instanceHelper, null);

        }

        public async Task<ResponseDto> AddBusinessAreaRelationshipAsync(BusinessAreaRelationshipDto data)
        {
            ResponseDto response = new ResponseDto();

            try
            {
                 BusinessAreaRelationship businessAreaRelationshipEntity = _mapper.Map<BusinessAreaRelationshipDto, BusinessAreaRelationship>(data);

                 var exist = await _repository.GetAllAsync();
                 exist = exist.Where(x => x.CustomerId == data.CustomerId &&
                                          x.BusinessAreaId == data.BusinessAreaId &&
                                          x.FilteredBusinessAreaId == data.FilteredBusinessAreaId).ToList();

                 if (exist.Any())
                     return response = await _responseMessageHelper.ResponseMessage(false,
                         new List<string[]>
                         {
                             new[]
                             {
                                 "adding business area relationship was not successful",
                                 "Relationship already exist for this customer"
                             }
                         });

                 bool result = false;
                 if (businessAreaRelationshipEntity.FilteredBusinessAreaId != null) result = await _repository.AddAsync(businessAreaRelationshipEntity);

                if (!result)
                {
                    response = await _responseMessageHelper.ResponseMessage(result,
                        new List<string[]> { new[] { "adding business area relationship was not successful", "Please try again later" } });
                }

                response = await _responseMessageHelper.ResponseMessage(result,
                    new List<string[]> { new[] { "adding business area relationship was successful", "Thank You" } });

                return response;
            }
            catch (Exception e)
            {
                return response;
            }
        }

        public async Task<ResponseDto> DeleteBusinessAreaRelationshipAsync(BusinessAreaRelationshipDto data)
        {
            ResponseDto response = new ResponseDto();

            try
            {
                BusinessAreaRelationship entity = _mapper.Map<BusinessAreaRelationshipDto, BusinessAreaRelationship>(data);
                bool result = await _repository.DeleteAsync(entity);

                if (!result)
                {
                    response = await _responseMessageHelper.ResponseMessage(result,
                        new List<string[]> { new[] { "deleting business area relationship was not successful", "Please try again later" } });
                }

                response = await _responseMessageHelper.ResponseMessage(result,
                    new List<string[]> { new[] { "deleting business area relationship was successful", "Thank You" } });

                return response;
            }
            catch (Exception e)
            {
                return response;
            }
        }

        public async Task<List<BusinessAreaRelationshipDto>> GetAllBusinessAreaRelationshipsAsync(int businessAreaId, int customerId)
        {
            try
            {
                List<BusinessAreaRelationshipDto> businessAreaRelationship = new List<BusinessAreaRelationshipDto>();

                List<BusinessAreaRelationship> result = await _repository.GetAllAsync();
                List<BusinessAreaRelationshipDto> businessRelationship = _mapper.Map<List<BusinessAreaRelationship>, List<BusinessAreaRelationshipDto>>(result);

                if (result.Count > 0)
                {
                    IEnumerable<BusinessAreaRelationshipDto> filteredBusinessArea = businessRelationship.Where(x => x.BusinessAreaId == businessAreaId && x.IsActive == true && x.CustomerId == customerId);
                    foreach (BusinessAreaRelationshipDto relationship in filteredBusinessArea)
                    {
                        CustomersDto customer = new CustomersDto();
                        customer = await _customerService.GetDirectoryAsync(relationship.CustomerId);
                        relationship.CustomerName = customer.Name;

                        BusinessAreaDto businessArea = await _businessAreaService.GetDirectoryAsync(businessAreaId);
                        relationship.BusinessAreaName = businessArea.Name;

                        relationship.customer = customer;

                        businessAreaRelationship.Add(relationship);
                    }

                    if (businessAreaRelationship.Count > 0) return businessAreaRelationship;

                }

                return businessRelationship;
            }
            catch (Exception e)
            {
                return new List<BusinessAreaRelationshipDto>();
            }
        }

        public async Task<List<BusinessAreaRelationshipDto>> GetAllDataBusinessAreaRelationshipsAsync()
        {
            List<BusinessAreaRelationshipDto> businessAreaRelationships = new List<BusinessAreaRelationshipDto>();
            try
            {
                List<BusinessAreaRelationship> result = await _repository.GetAllAsync();

                if (result.Count < 0)
                {
                    return businessAreaRelationships;
                }

                List<BusinessAreaRelationshipDto> businessAreaRelationship = _mapper.Map<List<BusinessAreaRelationship>, List<BusinessAreaRelationshipDto>>(result);

                foreach (BusinessAreaRelationshipDto relationship in businessAreaRelationship)
                {
                    CustomersDto customer = await _customerService.GetDirectoryAsync(relationship.CustomerId);
                    relationship.CustomerName = customer.Name;

                    BusinessAreaDto businessArea = await _businessAreaService.GetDirectoryAsync(relationship.BusinessAreaId.Value);
                    relationship.BusinessAreaName = businessArea.Name;

                    BusinessAreaDto filteredBusinessArea = await _businessAreaService.GetDirectoryAsync(relationship.FilteredBusinessAreaId.Value);
                    relationship.FilteredBusinessAreaName = filteredBusinessArea.Name;

                    relationship.customer = customer;

                    businessAreaRelationships.Add(relationship);
                }

                if (businessAreaRelationship.Count > 0) return businessAreaRelationships;

                return businessAreaRelationships;
            }
            catch (Exception e)
            {
                return businessAreaRelationships;
            }
        }

        public async Task<List<PersonDto>> GetCascadeFiltering(int businessArea1, int businessArea2, int businessArea3, int customerId, int applicationId)
        {
            List<PersonDto> cascadeFilterData = new List<PersonDto>();

            try
            {
                List<BusinessAreaRelationship> businessAreaResult = await _repository.GetAllAsync();
                List<ApplicationCustomerDto> applicationCustomerResult = await _applicationCustomerService.GetAllDirectoryAsync();

                cascadeFilterData = await _personService.GetAllDirectoryAsync();

                ApplicationCustomerDto filteredApplicationCustomerResult = applicationCustomerResult.Where(x =>
                    x.ApplicationId == applicationId
                    && x.CustomerId == customerId).FirstOrDefault();

                if (filteredApplicationCustomerResult == null) return new List<PersonDto>();

                cascadeFilterData = cascadeFilterData
                    .Where(x => x.CustomerId == filteredApplicationCustomerResult.CustomerId).ToList();

                if (!cascadeFilterData.Any()) return cascadeFilterData;


                if (businessArea1 != 0)
                {
                    cascadeFilterData =
                        cascadeFilterData.Where(x => x.BusinessArea1 == businessArea1).ToList();

                    if (!cascadeFilterData.Any()) return new List<PersonDto>();
                }

                if (businessArea2 != 0)
                {
                    cascadeFilterData =
                        cascadeFilterData.Where(x => x.BusinessArea2 == businessArea2).ToList();

                    if (!cascadeFilterData.Any()) return new List<PersonDto>();
                }

                if (businessArea3 != 0)
                {
                    cascadeFilterData =
                        cascadeFilterData.Where(x => x.BusinessArea3 == businessArea3).ToList();
                }

                return cascadeFilterData;
            }
            catch (Exception e)
            {
                return cascadeFilterData;
            }
        }



        public async Task<List<BusinessAreaRelationshipDto>> GetDropDownAsync(int businessArea1, int businessArea2, int customerId, int applicationId)
        {
            List<BusinessAreaRelationshipDto> dropdownBusinessAreas = new List<BusinessAreaRelationshipDto>();

            try
            {
                List<BusinessAreaRelationship> result = await _repository.GetAllAsync();

                if (!result.Any()) return dropdownBusinessAreas;

                List<BusinessAreaRelationshipDto> dropdownBusinessArea = _mapper.Map<List<BusinessAreaRelationship>, List<BusinessAreaRelationshipDto>>(result);


                foreach (BusinessAreaRelationshipDto dropdown in dropdownBusinessArea)
                {
                    CustomersDto customer = await _customerService.GetDirectoryAsync(dropdown.CustomerId);
                    dropdown.CustomerName = customer.Name;

                    BusinessAreaDto businessArea = await _businessAreaService.GetDirectoryAsync(dropdown.BusinessAreaId.Value);
                    dropdown.BusinessAreaName = businessArea.Name;

                    BusinessAreaDto businessAreaType = await _businessAreaService.GetDirectoryAsync(dropdown.BusinessAreaId.Value);
                    dropdown.BusinessAreaTypeId = businessAreaType.BusinessAreaTypeId;

                    BusinessAreaDto filteredBusinessArea = await _businessAreaService.GetDirectoryAsync(dropdown.FilteredBusinessAreaId.Value);
                    dropdown.FilteredBusinessAreaName = filteredBusinessArea.Name;
                    dropdown.FilteredBusinessAreaTypeId = filteredBusinessArea.BusinessAreaTypeId;
                }

                List<BusinessAreaDto> resultBusinessArea = await _businessAreaService.GetAllDirectoryAsync();
              
                dropdownBusinessAreas = dropdownBusinessArea
                    .Where(x => x.CustomerId == customerId && x.IsActive == true).DistinctBy(x => x.BusinessAreaName).ToList();


                if (!dropdownBusinessAreas.Any()) return dropdownBusinessAreas;

                BusinessAreaDto ba1 = resultBusinessArea.Where(x => x.Id == businessArea1).FirstOrDefault();
                if (businessArea1 != 0)
                {
                    dropdownBusinessAreas = new List<BusinessAreaRelationshipDto>();
                    
                    dropdownBusinessAreas = dropdownBusinessArea.Where(x =>
                        x.CustomerId == customerId && x.IsActive == true && x.BusinessAreaId == businessArea1 && 
                        x.FilteredBusinessAreaTypeId != ba1.BusinessAreaTypeId && 
                        x.FilteredBusinessAreaId != businessArea1).DistinctBy(y => y.FilteredBusinessAreaName).ToList();

                    if (!dropdownBusinessAreas.Any()) return dropdownBusinessAreas;
                }

                if (businessArea2 != 0)
                {
                    BusinessAreaDto ba2 = resultBusinessArea.Where(x => x.Id == businessArea2).FirstOrDefault();
                    dropdownBusinessAreas = new List<BusinessAreaRelationshipDto>();

                    dropdownBusinessAreas = dropdownBusinessArea.Where(x =>
                        x.CustomerId == customerId && x.IsActive == true && x.BusinessAreaId == businessArea2 &&
                        x.FilteredBusinessAreaTypeId != ba2.BusinessAreaTypeId &&
                        x.FilteredBusinessAreaTypeId != ba1.BusinessAreaTypeId &&
                        x.FilteredBusinessAreaId != businessArea1).DistinctBy(y => y.FilteredBusinessAreaName).ToList();
                }
                
                return dropdownBusinessAreas;

            }
            catch (Exception e)
            {
                return dropdownBusinessAreas;
            }
        }
    }
}
