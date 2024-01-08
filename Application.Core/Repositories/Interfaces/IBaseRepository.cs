using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Repositories.Interfaces
{
    public interface IBaseRepository<Entity> where Entity : class
    {
        Task<bool> AddAsync(Entity entity);
        Task<bool> UpdateAsync(Entity entity);
        Task<bool> DeleteAsync(Entity entity);
        Task<Entity> GetAsync(int id);
        Task<Entity> GetByNameAsync(Expression<Func<Entity, bool>> predicate = null);
        Task<List<Entity>> GetAllAsync();
        Task<int> SaveChangesAsync();
    }
}
