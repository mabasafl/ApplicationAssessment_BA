using Application.Data.Dtos.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Data.Models.Core;

namespace Application.Core.Interfaces
{
    public interface IBusinessAreaService
    {
        Task<ResponseDto> AddBusinessAreaAsync(BusinessAreaDto data);
        Task<ResponseDto> UpdateBusinessAreaAsync(BusinessArea data);
        Task<List<BusinessAreaDto>> GetAllBusinessAreasAsync();
        Task<BusinessAreaDto> GetBusinessAreaAsync(int id);
        Task<ResponseDto> DeleteBusinessAreaAsync(BusinessArea data);
    }
}
