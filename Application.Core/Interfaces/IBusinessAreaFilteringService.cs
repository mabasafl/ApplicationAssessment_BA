using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DataTransfer.Dtos.Core;
using Application.Data.Models.Core;

namespace Application.Core.Interfaces
{
    public interface IBusinessAreaFilteringService
    {
        Task<List<BusinessAreaRelationshipDto>> GetAllBusinessAreaRelationshipsAsync(int businessAreaId, int customerId);
        Task<List<BusinessAreaRelationshipDto>> GetAllDataBusinessAreaRelationshipsAsync();
        Task<List<PersonDto>> GetCascadeFiltering(int businessArea1, int businessArea2, int businessArea3, int customerId, int applicationId);
        Task<List<BusinessAreaRelationshipDto>> GetDropDownAsync(int businessArea1, int businessArea2, int customerId, int applicationId);
    }
}
