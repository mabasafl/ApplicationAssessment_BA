using System;
using System.Collections.Generic;

namespace Application.Data.Models.Core
{
    public class Person :BaseEntity
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;
        public string MobileNumber { get; set; } = null!;
        public int CustomerId { get; set; }

    }
}
