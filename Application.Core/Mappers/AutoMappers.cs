using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Core.DTOs;
using Application.Core.Models;
using AutoMapper;

namespace Application.Core.Mappers
{
    public class AutoMappers: Profile
    {
        public AutoMappers()
        {
            CreateMap<Applications, ApplicationsDto>().ReverseMap();
        }
    }
}
