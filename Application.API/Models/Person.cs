using System;
using System.Collections.Generic;

namespace Application.API.Models
{
    public partial class Person
    {
        public int Id { get; set; }
        public string AlternateId { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;
        public string MobileNumber { get; set; } = null!;
        public int CustomerId { get; set; }
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime? DateModified { get; set; }
        public string? ModifiedBy { get; set; }

        public virtual Customer Customer { get; set; } = null!;
    }
}
