using THH.Core.Interfaces;

namespace THH.Shared.BLL.Interfaces;

public interface IGenericCommandService<T>
         where T : class, IEntityBase, new()
{
    public Task<T> AddAsync<D>(D dto) where D : IDTO;
    public Task UpdateAsync<D>(D dto) where D : IDTO;
    public Task RemoveAsync<D>(D dto, bool hardDelete = false) where D : IDTO;
    Task<int> SaveChangesAsync();
}
