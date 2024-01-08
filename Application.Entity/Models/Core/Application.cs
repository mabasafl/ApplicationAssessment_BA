using System;
using System.Collections.Generic;

namespace Application.Data.Models.Core
{
    public class 
        Applications
    {

        public int Id { get; set; }
        public string AlternateId { get; set; } = new Guid().ToString();
        public string Name { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime? DateModified { get; set; }
        public string? ModifiedBy { get; set; }

    }
}
