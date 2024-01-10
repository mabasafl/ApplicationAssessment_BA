using Application.Data.Dtos.Core;
using System;
using System.Collections.Generic;

namespace Application.Data.Models.Core
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; } = null!;

    }
}
