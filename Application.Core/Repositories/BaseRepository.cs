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
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Application.Core.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly DataContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(DataContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }
        public async Task<bool> AddAsync(T entity)
        {
            ValueTask<EntityEntry<T>> add = _dbSet.AddAsync(entity);
            int saved = await SaveChangesAsync();
            if (saved != 0)
                return true;
            return false;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            EntityEntry<T> delete = _dbSet.Remove(entity);
            int removed = await SaveChangesAsync();
            if (removed != 0)
                return true;
            return false;
        }

        public async Task<T> GetAsync(int id)
        {
            T entity = await _dbSet.FindAsync(id);
            return entity;
        }

        public async Task<T> GetByNameAsync(Expression<Func<T,bool>> predicate =null)
        {
            T entity = await _dbSet.FirstOrDefaultAsync(predicate);
            return entity;
        }

        public async Task<List<T>> GetAllAsync()
        {
            List<T> list = await _dbSet.ToListAsync();
            return list;
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            EntityEntry<T> update = _dbSet.Update(entity);
            int modified = await SaveChangesAsync();
            if (modified != 0)
                return true;
            return false;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
