using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Dtos.Core
{
    public class ApplicationCustomerDto : BaseDto
    {
        public int ApplicationId { get; set; }
        public int CustomerId { get; set; }
        public int BusinessAreaId { get; set; }
    }
}
