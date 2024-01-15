using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Data.Models.Core;
using Application.DataTransfer.Dtos.Core;

namespace Application.Core.Interfaces
{
    public interface IApplicationCustomerService
    {
        Task<List<ApplicationCustomerDto>> GetApplicationByFriendlyUrl(string friendlyUrl);
    }
}
