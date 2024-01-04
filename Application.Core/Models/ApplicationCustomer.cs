using System;
using System.Collections.Generic;

namespace Application.Core.Models
{
    public partial class ApplicationCustomer
    {
        public int Id { get; set; }
        public string AlternateId { get; set; } = null!;
        public int ApplicationId { get; set; }
        public int CustomerId { get; set; }
        public int BusinessAreaId { get; set; }
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime? DateModified { get; set; }
        public string? ModifiedBy { get; set; }

        //public virtual Applications Application { get; set; } = null!;
        //public virtual BusinessArea BusinessArea { get; set; } = null!;
        //public virtual Customer Customer { get; set; } = null!;
    }
}
