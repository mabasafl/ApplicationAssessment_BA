using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Dtos.Core
{
    public class BusinessAreaRelationshipDto : BaseDto
    {
        public int CustomerId { get; set; }
        public int? BusinessAreaId { get; set; }
        public int? FilteredBusinessAreaId { get; set; }
        public bool? IsActive { get; set; }
    }
}
