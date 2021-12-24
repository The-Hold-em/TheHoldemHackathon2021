using AutoMapper;

using THH.Shared.BLL.Interfaces;
using THH.Shared.DAL.Interfaces;
using THH.Core.Interfaces;

namespace THH.Shared.BLL.Managers;

public class GenericCommandManager<T> : IGenericCommandService<T>
 where T : class, IEntityBase, new()
{
    private readonly IGenericCommandRepository<T> _genericRepository;
    private readonly IGenericQueryRepository<T> _genericQueryRepository;
    private readonly IMapper _mapper;
    private readonly ICustomMapper _customMapper;

    public GenericCommandManager(
        IGenericCommandRepository<T> genericRepository,
        IGenericQueryRepository<T> genericQueryRepository,
        IMapper mapper,
        ICustomMapper customMapper)
    {
        this._genericRepository = genericRepository;
        this._genericQueryRepository = genericQueryRepository;
        this._mapper = mapper;
        this._customMapper = customMapper;
    }

    public async Task<T> AddAsync<D>(D dto) where D : IDTO
    {
        T entity = _mapper.Map<T>(dto);
        await _genericRepository.AddAsync(entity);
        return entity;
    }

    public async Task RemoveAsync<D>(D dto, bool hardDelete = false) where D : IDTO
    {
        T dummyEntity = _mapper.Map<T>(dto);
        T? orjinal = await _genericQueryRepository.GetByIdAsync(dummyEntity.Id);
        if (orjinal is null) throw new NullReferenceException(nameof(orjinal));
        orjinal = _customMapper.Map(dto, orjinal);
        await _genericRepository.RemoveAsync(orjinal, hardDelete);
    }


    public async Task UpdateAsync<D>(D dto) where D : IDTO
    {
        T dummyEntity = _mapper.Map<T>(dto);
        T? orjinal = await _genericQueryRepository.GetByIdAsync(dummyEntity.Id);
        if (orjinal is null) throw new NullReferenceException(nameof(orjinal));
        orjinal = _customMapper.Map(dto, orjinal);
        await _genericRepository.UpdateAsync(orjinal);
    }
    public Task<int> SaveChangesAsync() => _genericRepository.SaveChangesAsync();
}
