#nullable disable
using THH.Shared.DAL.Interfaces;
using THH.Core.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace THH.Shared.DAL.Concrete.EntityFrameworkCore.Repositories;

public class EfGenericCommandRepository<T> : IGenericCommandRepository<T>
   where T : class, IEntityBase, new()
{
    private DbContext dbContext { get; init; }
    private DbSet<T> table { get; init; }

    public EfGenericCommandRepository(DbContext dbContext)
    {
        this.dbContext = dbContext;
        table = dbContext.Set<T>();
    }

    #region CRUD

    public async Task<T> AddAsync(T entity)
    {
        entity.CreatedTime = DateTime.Now;
        await table.AddAsync(entity);
        return entity;
    }

    public async Task AddRangeAsync(IEnumerable<T> entities)
    {
        foreach (var item in entities)
            item.CreatedTime = DateTime.Now;
        await table.AddRangeAsync(entities);
    }

    public async Task UpdateAsync(T entity)
    {
        entity.UpdatedTime = DateTime.Now;
        await Task.FromResult(table.Update(entity));
    }

    public async Task RemoveAsync(T entity, bool hardDelete = false)
    {
        if (hardDelete)
            table.Remove(entity);
        else
        {
            entity.IsDeleted = true;
            await UpdateAsync(entity);
        }
    }

    #endregion

    #region Dispose
    public async ValueTask DisposeAsync() => await dbContext.DisposeAsync();
    #endregion

    #region Save
    public async Task<int> SaveChangesAsync() => await dbContext.SaveChangesAsync();
    #endregion

}
