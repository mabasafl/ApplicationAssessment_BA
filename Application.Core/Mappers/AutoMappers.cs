﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Data.Dtos.Core;
using Application.Data.Models.Core;
using AutoMapper;

namespace Application.Core.Mappers
{
    public class AutoMappers: Profile
    {
        public AutoMappers()
        {
            CreateMap<Applications, ApplicationsDto>().ReverseMap();
            CreateMap<Customer, CustomersDto>().ReverseMap();
            CreateMap<Person, PersonDto>().ReverseMap();
        }
    }
}