using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Application.Core.Interfaces;
using Application.Core.Repositories.Interfaces;
using Application.Data.Models.Core;
using Application.DataTransfer.Dtos.Core;
using AutoMapper;

namespace Application.Core.Services
{
    public class ApplicationCustomerService : IApplicationCustomerService
    {
        private readonly IDirectoryService<Customer, CustomersDto> _customerService;
        private readonly IDirectoryService<Applications, ApplicationsDto> _applicationsService;
        private readonly IBaseRepository<ApplicationCustomer> _repository;
        private readonly IMapper _mapper;
        public ApplicationCustomerService(IDirectoryService<Customer, CustomersDto> customerService, IDirectoryService<Applications, ApplicationsDto> applicationsService, IBaseRepository<ApplicationCustomer> repository, IMapper mapper)
        {
            _customerService = customerService;
            _applicationsService = applicationsService;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ApplicationCustomerDto>> GetApplicationByFriendlyUrl(string friendlyUrl)
        {
            try
            {
                Expression<Func<Applications, bool>> predicate = x => x.Name == friendlyUrl;

                ApplicationsDto application = await _applicationsService.GetDirectoryByNameAsync(predicate);

                if (application == null) return new List<ApplicationCustomerDto>();

                List<ApplicationCustomer> result = await _repository.GetAllAsync();

                result = result.Where(x => x.ApplicationId == application.Id).ToList();

                if (!result.Any()) return new List<ApplicationCustomerDto>();

                List<ApplicationCustomerDto> applicationCustomers =
                    _mapper.Map<List<ApplicationCustomer>, List<ApplicationCustomerDto>>(result);


                List<CustomersDto> customersResult = await _customerService.GetAllDirectoryAsync();

                if (!customersResult.Any()) return applicationCustomers;

                List<CustomersDto> customers = customersResult.ToList();


                foreach (ApplicationCustomerDto applicationCustomer in applicationCustomers)
                {


                    CustomersDto customer = customers.Where(x => x.Id == applicationCustomer.CustomerId).FirstOrDefault();

                    applicationCustomer.FriendlyUrl = friendlyUrl;

                    applicationCustomer.CustomerName = customer.Name;
                }

                return applicationCustomers;
            }
            catch (Exception e)
            {
                return new List<ApplicationCustomerDto>();
            }


        }
    }
}
