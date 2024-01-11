using Application.Data.Dtos.Core;
using Application.Data.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Interfaces
{
    public interface IBusinessAreaTypeService
    {
        Task<ResponseDto> AddBusinessAreaTypeAsync(BusinessAreaTypeDto businessAreaType);
        Task<ResponseDto> UpdateBusinessAreaTypeAsync(BusinessAreaType businessAreaType);
        Task<List<BusinessAreaTypeDto>> GetAllBusinessAreaTypesAsync();
        Task<BusinessAreaTypeDto> GetBusinessAreaTypeAsync(int id);
        Task<ResponseDto> DeleteBusinessAreaTypeAsync(BusinessAreaType businessAreaType);
    }
}
