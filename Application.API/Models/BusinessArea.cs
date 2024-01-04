using System;
using System.Collections.Generic;

namespace Application.API.Models
{
    public partial class BusinessArea
    {
        public BusinessArea()
        {
            ApplicationCustomers = new HashSet<ApplicationCustomer>();
            BusinessAreaRelationshipBusinessAreas = new HashSet<BusinessAreaRelationship>();
            BusinessAreaRelationshipFilteredBusinessAreas = new HashSet<BusinessAreaRelationship>();
        }

        public int Id { get; set; }
        public string AlternateId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int BusinessAreaTypeId { get; set; }
        public int CustomerId { get; set; }
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime? DateModified { get; set; }
        public string? ModifiedBy { get; set; }

        public virtual BusinessAreaType BusinessAreaType { get; set; } = null!;
        public virtual Customer Customer { get; set; } = null!;
        public virtual ICollection<ApplicationCustomer> ApplicationCustomers { get; set; }
        public virtual ICollection<BusinessAreaRelationship> BusinessAreaRelationshipBusinessAreas { get; set; }
        public virtual ICollection<BusinessAreaRelationship> BusinessAreaRelationshipFilteredBusinessAreas { get; set; }
    }
}
