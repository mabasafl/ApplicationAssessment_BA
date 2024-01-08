using System;
using System.Collections.Generic;

namespace Application.Data.Models.Core
{
    public class ApplicationCustomer
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

    }
}
