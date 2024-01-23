using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataTransfer.Dtos.Core
{
    public class BaseDto
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; } = null!;
        public DateTime? DateModified { get; set; } = DateTime.Now;
        public string? ModifiedBy { get; set; } = null;

    }
}
