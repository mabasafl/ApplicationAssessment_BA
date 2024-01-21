using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Application.Data.Models.Core;
using Application.DataTransfer.Dtos.Core;

namespace Application.Core.Interfaces
{
    public interface IBusinessAreaTypeRelationshipService
    {
        Task<List<BusinessAreaTypeRelationshipDto>> GetAllBusinessAreaTypeRelationshipAsync(int customerId);
        Task<ResponseDto> AddBusinessAreaTypeRelationshipAsync(BusinessAreaTypeRelationshipDto data);
        Task<ResponseDto> UpdateBusinessAreaTypeRelationshipAsync(BusinessAreaTypeRelationship data);
        Task<BusinessAreaTypeRelationshipDto> GetBusinessAreaTypeRelationshipAsync(int id);
        Task<ResponseDto> DeleteBusinessAreaTypeRelationshipAsync(BusinessAreaTypeRelationship data);

    }
}
