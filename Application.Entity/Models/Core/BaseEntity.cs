using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Models.Core
{
    public class BaseEntity 
    {
        public int Id { get; set; }
        public string AlternateId { get; set; } = Guid.NewGuid().ToString();
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime? DateModified { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
