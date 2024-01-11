using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Data.Dtos.Core;
using Application.Data.Models.Core;

namespace Application.Core.Interfaces
{
    public interface IBusinessAreaFilteringService
    {
        Task<List<BusinessAreaRelationshipDto>> GetAllBusinessAreaRelationshipsAsync(int businessAreaId);
        Task<List<BusinessAreaRelationshipDto>> GetAllDataBusinessAreaRelationshipsAsync();
    }
}
