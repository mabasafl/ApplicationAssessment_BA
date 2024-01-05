using Application.Data.Dtos.Core;
using Application.Data.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Interfaces
{
    public interface IPersonService
    {
        Task<ResponseDto> AddPersonAsync(PersonDto person);
        Task<ResponseDto> UpdatePersonAsync(Person person);
        Task<List<PersonDto>> GetAllPersonAsync();
        Task<PersonDto> GetPersonAsync(int id);
        Task<ResponseDto> DeletePersonAsync(Person person);
    }
}
