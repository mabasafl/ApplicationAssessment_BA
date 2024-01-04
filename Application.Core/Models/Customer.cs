using System;
using System.Collections.Generic;

namespace Application.Core.Models
{
    public partial class Customer
    {
        //public Customer()
        //{
        //    ApplicationCustomers = new HashSet<ApplicationCustomer>();
        //    BusinessAreaRelationships = new HashSet<BusinessAreaRelationship>();
        //    BusinessAreas = new HashSet<BusinessArea>();
        //    People = new HashSet<Person>();
        //}

        public int Id { get; set; }
        public string AlternateId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime? DateModified { get; set; }
        public string? ModifiedBy { get; set; }

        //public virtual ICollection<ApplicationCustomer> ApplicationCustomers { get; set; }
        //public virtual ICollection<BusinessAreaRelationship> BusinessAreaRelationships { get; set; }
        //public virtual ICollection<BusinessArea> BusinessAreas { get; set; }
        //public virtual ICollection<Person> People { get; set; }
    }
}
