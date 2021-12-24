using THH.Core.Interfaces;

namespace THH.Shared.DAL.Interfaces;

public interface IGenericCommandRepository<T> 
       where T : class, IEntityBase, new()
{
    Task<T> AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entities);
    Task UpdateAsync(T entity);
    Task RemoveAsync(T entity, bool hardDelete = false);
    Task<int> SaveChangesAsync();
}
