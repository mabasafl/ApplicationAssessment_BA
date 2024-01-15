using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataTransfer.Dtos.Core
{
    public class CascadeFilterDto
    {
        public BusinessAreaRelationshipDto BusinessAreaRelationship { get; set; }
        public List<CustomersDto> Customer { get; set; }
        public List<PersonDto> Person { get; set; }
    }
}
