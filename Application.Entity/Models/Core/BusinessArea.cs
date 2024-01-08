using System;
using System.Collections.Generic;

namespace Application.Data.Models.Core
{
    public class BusinessArea: BaseEntity
    {

        public string Name { get; set; } = null!;
        public int BusinessAreaTypeId { get; set; }
        public int CustomerId { get; set; }
    }
}
