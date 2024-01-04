using System;
using System.Collections.Generic;

namespace Application.API.Models
{
    public partial class Application
    {
        public Application()
        {
            ApplicationCustomers = new HashSet<ApplicationCustomer>();
        }

        public int Id { get; set; }
        public string AlternateId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime? DateModified { get; set; }
        public string? ModifiedBy { get; set; }

        public virtual ICollection<ApplicationCustomer> ApplicationCustomers { get; set; }
    }
}
