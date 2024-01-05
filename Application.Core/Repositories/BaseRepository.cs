using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Application.Data.Data;
using Application.Core.Repositories.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Application.Core.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly DataContext _dbContext;
        private readonly DbSet<T> _dbSet;
        private readonly IMapper _mapper;

        public BaseRepository(DataContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
            _mapper = mapper;
        }
        public async Task<bool> AddAsync(T entity)
        {
            var add = _dbSet.AddAsync(entity);
            var saved = await _dbContext.SaveChangesAsync();
            if (saved != 0)
                return true;
            return false;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            var delete = _dbSet.Remove(entity);
            var removed = await _dbContext.SaveChangesAsync();
            if (removed != 0)
                return true;
            return false;
        }

        public async Task<T> GetAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            return entity;
        }

        public async Task<List<T>> GetAllAsync()
        {
            var list = await _dbSet.ToListAsync();
            return list;
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            var update = _dbSet.Update(entity);
            var modified = await _dbContext.SaveChangesAsync();
            if (modified != 0)
                return true;
            return false;
        }
    }
}
