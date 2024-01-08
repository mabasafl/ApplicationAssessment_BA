﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Dtos.Core
{
    public class BusinessAreaDto: BaseDto
    {
        public string Name { get; set; } = null!;
        public int BusinessAreaTypeId { get; set; }
        public int CustomerId { get; set; }
    }
}
