using System;
using System.Collections.Generic;

namespace Application.Data.Models.Core
{
    public class BusinessAreaRelationship : BaseEntity
    {
        public int CustomerId { get; set; }
        public int? BusinessAreaId { get; set; }
        public int? FilteredBusinessAreaId { get; set; }
        public bool? IsActive { get; set; }

    }
}
