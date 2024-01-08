using System;
using System.Collections.Generic;

namespace Application.Data.Models.Core
{
    public class ApplicationCustomer : BaseEntity
    {
        public int ApplicationId { get; set; }
        public int CustomerId { get; set; }
        public int BusinessAreaId { get; set; }

    }
}
