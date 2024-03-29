﻿using Application.DataTransfer.Dtos.Core;
using Application.Data.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Application.Core.Interfaces
{
    public interface IDirectoryService<Entity, Dto> where Entity : class
    {
        Task<ResponseDto> AddDirectoryAsync(Dto directoryData);
        Task<ResponseDto> UpdateDirectoryAsync(Entity directoryData);
        Task<List<Dto>> GetAllDirectoryAsync();
        Task<Dto> GetDirectoryAsync(int id);
        Task<Dto> GetDirectoryByNameAsync(Expression<Func<Entity, bool>> predicate);
        Task<ResponseDto> DeleteDirectoryAsync(Entity directoryData);
    }
}
