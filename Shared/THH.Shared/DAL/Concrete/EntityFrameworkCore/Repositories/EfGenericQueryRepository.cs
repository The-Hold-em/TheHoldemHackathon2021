using THH.Core.Interfaces;
using THH.Shared.DAL.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace THH.Shared.DAL.Concrete.EntityFrameworkCore.Repositories;

public class EfGenericQueryRepository<T> : IGenericQueryRepository<T>
 where T : class, IEntityBase, new()
{
    private readonly DbContext _dbContext;
    private readonly DbSet<T> _table;


    public EfGenericQueryRepository(DbContext dbContext)
    {
        this._dbContext = dbContext;
        _table = dbContext.Set<T>();

    }


    #region GetAll
    public Task<IEnumerable<T>> GetAllAsync(params string[] inculudes)
    {
        var result = _table.Where(x => !x.IsDeleted);
        foreach (var item in inculudes)
            result = result.Include(item);
        return Task.FromResult(result.AsEnumerable());
    }

    public Task<IEnumerable<T>> GetAllWithDeletedAsync() => Task.FromResult(_table.AsEnumerable());
    #endregion

    #region Get
    public ValueTask<T?> GetByIdAsync(Guid id) => _table.FindAsync(id);
    #endregion

    #region Dispose

    public ValueTask DisposeAsync() => _dbContext.DisposeAsync();
    #endregion
}
