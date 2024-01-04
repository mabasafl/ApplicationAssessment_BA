using Application.Data.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Data.Dtos.Core;

namespace Application.Core.Interfaces
{
    public interface IApplicationService
    {
        Task<ResponseDto> AddApplicationAsync(ApplicationsDto application);
        Task<ResponseDto> UpdateApplicationAsync(Applications application );
        Task<List<ApplicationsDto>> GetAllApplicationsAsync();
        Task<ApplicationsDto> GetApplicationsAsync(int id);
        Task<ResponseDto> DeleteApplicationAsync(Applications application);
    }
}
