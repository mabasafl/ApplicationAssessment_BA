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
    public class BusinessAreaTypeService : IBusinessAreaTypeService
    {
        private readonly IBaseRepository<BusinessAreaType> _repository;
        private readonly IMapper _mapper;
        public BusinessAreaTypeService(IBaseRepository<BusinessAreaType> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public Task<ResponseDto> AddBusinessAreaTypeAsync(BusinessAreaTypeDto businessAreaType)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto> DeleteBusinessAreaTypeAsync(BusinessAreaType businessAreaType)
        {
            throw new NotImplementedException();
        }

        public async Task<List<BusinessAreaTypeDto>> GetAllBusinessAreaTypesAsync()
        {
            List<BusinessAreaTypeDto> businessAreaTypes = new List<BusinessAreaTypeDto>();
            var result = await _repository.GetAllAsync();
            if (result != null)
            {
                businessAreaTypes = _mapper.Map<List<BusinessAreaType>, List<BusinessAreaTypeDto>>(result);
            }
            return businessAreaTypes;
        }

        public async Task<BusinessAreaTypeDto> GetBusinessAreaTypeAsync(int id)
        {
            BusinessAreaTypeDto businessAreaType = new BusinessAreaTypeDto();
            var result = await _repository.GetAsync(id);

            if (result != null)
            {
                businessAreaType = _mapper.Map<BusinessAreaType, BusinessAreaTypeDto>(result);
            }
            return businessAreaType;
        }

        public Task<ResponseDto> UpdateBusinessAreaTypeAsync(BusinessAreaType businessAreaType)
        {
            throw new NotImplementedException();
        }
    }
}
