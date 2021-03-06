using THH.Core.Interfaces;

namespace THH.Shared.BLL.Interfaces;

public interface IGenericQueryService<T>
       where T : class, IEntityBase, new()
{
    public Task<IEnumerable<D>> GetAllAsync<D>(params string[] inculudes) where D : IDTO;
    public Task<IEnumerable<D>> GetAllWithDeletedAsync<D>() where D : IDTO;
    public Task<D> GetByIdAsync<D>(Guid id) where D : IDTO;

}
