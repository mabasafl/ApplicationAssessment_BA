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
using static System.Net.Mime.MediaTypeNames;

namespace Application.Core.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IBaseRepository<Customer> _repository;
        private readonly IResponseMessageService _responseMessageService;
        private readonly IMapper _mapper;

        public CustomerService(IBaseRepository<Customer> repository, IMapper mapper)
        {
            _repository = repository;
            _responseMessageService = new ResponseMessageService();
            _mapper = mapper;
        }
        public async Task<List<CustomersDto>> GetAllCustomersAsync()
        {
            List<CustomersDto> customer = new List<CustomersDto>();
            var result = await _repository.GetAllAsync();
            if (result != null)
            {
                customer = _mapper.Map<List<Customer>, List<CustomersDto>>(result);
            }
            return customer;
        }

        public async Task<CustomersDto> GetCustomerAsync(int id)
        {
            CustomersDto customer = new CustomersDto();
            var result = await _repository.GetAsync(id);

            if (result != null)
            {
                customer = _mapper.Map<Customer, CustomersDto>(result);
            }
            return customer;
        }
        public async Task<ResponseDto> AddCustomerAsync(CustomersDto customerRequest)
        {
            ResponseDto response = null;
            //CustomersDto customerDto = new CustomersDto();

            Customer customer= _mapper.Map<CustomersDto, Customer>(customerRequest);
            customer.AlternateId = Guid.NewGuid().ToString();

            var result = await _repository.AddAsync(customer);

            if (result)
            {
                response = await _responseMessageService.ResponseMessage(result,
                    new List<string[]> { new[] { "adding customer was successful", "Thank You" } });
            }
            else
            {
                response = await _responseMessageService.ResponseMessage(result,
                    new List<string[]> { new[] { "adding customer was not successful", "Please try again later" } });
            }

            return response;
        }

        public async Task<ResponseDto> DeleteCustomerAsync(Customer customerRequest)
        {
            ResponseDto response = null;
            var result = await _repository.DeleteAsync(customerRequest);
            if (result)
            {
                response = await _responseMessageService.ResponseMessage(result, new List<string[]> { new[] { "deleting customer was successful", "Thank you!" } });
            }
            else
            {
                response = await _responseMessageService.ResponseMessage(result,
                    new List<string[]> { new[] { "deleting customer was not successful", "Please try again later" } });
            }

            return response;
        }

        public async Task<ResponseDto> UpdateCustomerAsync(Customer customerRequest)
        {
            ResponseDto response = null;
            var result = await _repository.UpdateAsync(customerRequest);

            if (result)
            {
                response = await _responseMessageService.ResponseMessage(result, new List<string[]> { new[] { "updating customer was successful", "Thank you!" } });
            }
            else
            {
                response = await _responseMessageService.ResponseMessage(result,
                    new List<string[]> { new[] { "updating customer was not successful", "Please try again later" } });
            }

            return response;
        }
    }
}
