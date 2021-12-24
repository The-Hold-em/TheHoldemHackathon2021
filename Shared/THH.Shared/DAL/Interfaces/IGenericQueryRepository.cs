using THH.Core.Interfaces;

namespace THH.Shared.DAL.Interfaces;

public interface IGenericQueryRepository<T> : IAsyncDisposable
        where T : class, IEntityBase, new()
{
     Task<IEnumerable<T>> GetAllAsync();
     Task<IEnumerable<T>> GetAllWithDeletedAsync();
     ValueTask<T?> GetByIdAsync(Guid id);
}