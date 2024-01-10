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

namespace Application.Core.Services
{
    public class BusinessAreaService : IBusinessAreaService
    {
        private readonly IBaseRepository<BusinessArea> _repository;
        private readonly IMapper _mapper;
        public BusinessAreaService(IBaseRepository<BusinessArea> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        public Task<ResponseDto> AddBusinessAreaAsync(BusinessAreaDto data)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto> DeleteBusinessAreaAsync(BusinessArea data)
        {
            throw new NotImplementedException();
        }

        public Task<List<BusinessAreaDto>> GetAllBusinessAreasAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<BusinessAreaDto> GetBusinessAreaAsync(int id)
        {
            BusinessAreaDto businessArea = new BusinessAreaDto();
            var result = await _repository.GetAsync(id);

            if (result != null)
            {
                businessArea = _mapper.Map<BusinessArea, BusinessAreaDto>(result);
            }
            return businessArea;
        }

        public Task<ResponseDto> UpdateBusinessAreaAsync(BusinessArea data)
        {
            throw new NotImplementedException();
        }
    }
}
