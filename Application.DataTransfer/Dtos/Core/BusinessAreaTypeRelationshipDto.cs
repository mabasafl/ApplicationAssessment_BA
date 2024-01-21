using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataTransfer.Dtos.Core
{
    public class BusinessAreaTypeRelationshipDto : BaseDto
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int BusinessAreaId { get; set; }
        public string BusinessAreaName { get; set; }
        public bool BusinessAreaType1 { get; set; }
        public bool BusinessAreaType2 { get; set; }
        public bool BusinessAreaType3 { get; set; }
        public bool IsActive { get; set; }
    }
}
