using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Core.Interfaces;
using Application.Core.Repositories.Interfaces;
using Application.Data.Dtos.Core;
using Application.Data.Models.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions.Impl;

namespace Application.Core.Services
{
    public class BusinessAreaFilteringService : IBusinessAreaFilteringService
    {
        private readonly IBaseRepository<BusinessAreaRelationship> _repository;
        private readonly IMapper _mapper;
        private readonly ICustomerService _customerServices;
        private readonly IBusinessAreaService _businessAreaService;
        public BusinessAreaFilteringService(IBaseRepository<BusinessAreaRelationship> repository, IMapper mapper, IBaseRepository<Customer> customerRepository, IBaseRepository<BusinessArea> businessAreaRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _customerServices = new CustomerService(customerRepository, _mapper);
            _businessAreaService = new BusinessAreaService(businessAreaRepository, _mapper);
        }
        public async Task<List<BusinessAreaRelationshipDto>> GetAllBusinessAreaRelationshipsAsync(int businessAreaId)
        {
            try
            {
                List<BusinessAreaRelationshipDto> businessAreaRelationship = new List<BusinessAreaRelationshipDto>();

                var result = await _repository.GetAllAsync();
                var businessRelationship = _mapper.Map<List<BusinessAreaRelationship>, List<BusinessAreaRelationshipDto>>(result);
                
                if (result.Count > 0)
                {
                    var filteredBusinessArea = businessRelationship.Where(x => x.BusinessAreaId == businessAreaId && x.IsActive == true);
                    foreach (var relationship in filteredBusinessArea)
                    {
                        var customer = await _customerServices.GetCustomerAsync(relationship.CustomerId);
                        relationship.CustomerName = customer.Name;

                        var businessArea = await _businessAreaService.GetBusinessAreaAsync(businessAreaId);
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
    }
}
