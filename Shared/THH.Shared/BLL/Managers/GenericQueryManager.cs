using AutoMapper;

using THH.Shared.BLL.Interfaces;
using THH.Core.Interfaces;
using THH.Shared.DAL.Interfaces;


namespace THH.Shared.BLL.Managers;

public class GenericQueryManager<T> : IGenericQueryService<T>
where T : class, IEntityBase, new()
{
    private readonly IGenericQueryRepository<T> _genericRepository;
    private readonly IMapper _mapper;

    public GenericQueryManager(IGenericQueryRepository<T> genericRepository, IMapper mapper)
    {
        this._genericRepository = genericRepository;
        this._mapper = mapper;
    }

    public async Task<IEnumerable<D>> GetAllAsync<D>(params string[] inculudes) where D : IDTO
    {
        IEnumerable<T> entities = (await _genericRepository.GetAllAsync(inculudes)).ToList();
        IEnumerable<D> result = _mapper.Map<IEnumerable<D>>(entities);
        return result;
    }

    public async Task<IEnumerable<D>> GetAllWithDeletedAsync<D>() where D : IDTO
    {
        IEnumerable<T> entities = await _genericRepository.GetAllWithDeletedAsync();
        IEnumerable<D> result = _mapper.Map<IEnumerable<D>>(entities);
        return result;
    }

    public async Task<D> GetByIdAsync<D>(Guid id) where D : IDTO
    {
        T? entity = await _genericRepository.GetByIdAsync(id);
        D result = _mapper.Map<D>(entity);
        return result;
    }
}
