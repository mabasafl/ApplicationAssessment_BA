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

namespace Application.Core.Services
{
    public class BusinessAreaFilteringService : IBusinessAreaFilteringService
    {
        private readonly IBaseRepository<BusinessAreaRelationship> _repository;
        private readonly IMapper _mapper;
        private readonly IDirectoryService<Customer, CustomersDto> _customerService;
        private readonly IDirectoryService<BusinessArea, BusinessAreaDto> _businessAreaService;
        private readonly INewInstanceHelper _instanceHelper;
        public BusinessAreaFilteringService(IBaseRepository<BusinessAreaRelationship> repository, IMapper mapper, 
            INewInstanceHelper instanceHelper, IBaseRepository<Customer> customerRepository, IBaseRepository<BusinessArea> businessAreaRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _instanceHelper = instanceHelper;
            _customerService = new DirectoryService<Customer, CustomersDto>(customerRepository, _mapper, _instanceHelper);
                _businessAreaService =
            new DirectoryService<BusinessArea, BusinessAreaDto>(businessAreaRepository, _mapper, _instanceHelper);

        }
        public async Task<List<BusinessAreaRelationshipDto>> GetAllBusinessAreaRelationshipsAsync(int businessAreaId)
        {
            try
            {
                List<BusinessAreaRelationshipDto> businessAreaRelationship = new List<BusinessAreaRelationshipDto>();

                List<BusinessAreaRelationship> result = await _repository.GetAllAsync();
                List<BusinessAreaRelationshipDto> businessRelationship = _mapper.Map<List<BusinessAreaRelationship>, List<BusinessAreaRelationshipDto>>(result);

                if (result.Count > 0)
                {
                    IEnumerable<BusinessAreaRelationshipDto> filteredBusinessArea = businessRelationship.Where(x => x.BusinessAreaId == businessAreaId && x.IsActive == true);
                    foreach (BusinessAreaRelationshipDto relationship in filteredBusinessArea)
                    {
                        CustomersDto customer = await _customerService.GetDirectoryAsync(relationship.CustomerId);
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
    }
}
