using AutoMapper;
using Repository.Repositories;

namespace Core.Services;

/// <summary>
///     Generic service for CRUD operations. Generally all operations follow the same pattern and throw the same
///     exceptions, so we can reuse this class.
/// </summary>
/// <param name="repository">Repository class to perform the operations</param>
/// <param name="mapper">Mapper to map to and from DTO/Entity</param>
/// <typeparam name="TEntity">Type of entity</typeparam>
/// <typeparam name="TId">Type of primary key for the entity</typeparam>
/// <remarks>
///     Uses generic methods because they might take a different shape of DTO. This is almost certainly the wrong way
///     to do this.
/// </remarks>
public class CrudService<TEntity, TId>(ICrudRepository<TEntity, TId> repository, IMapper mapper)
    : ICrudService<TEntity, TId> where TEntity : class
{
    public virtual async Task<TEntity> GetByIdAsync(TId id)
    {
        var entity = await repository.GetByIdAsync(id);
        if (entity == null) throw new KeyNotFoundException();
        return entity;
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await repository.GetAllAsync();
    }

    // A little spooky if id arg and id property are not the same.
    // This is checked in controller.
    public virtual async Task UpdateAsync<TUpdateDto>(TId id, TUpdateDto entity) where TUpdateDto : class
    {
        var exists = await repository.GetByIdAsync(id) != null;
        if (!exists) throw new KeyNotFoundException();
        var updatedEntity = mapper.Map<TEntity>(entity);
        repository.Update(updatedEntity);
        await repository.SaveAsync();
    }

    public virtual async Task DeleteAsync(TId id)
    {
        var entity = await repository.GetByIdAsync(id);
        if (entity == null) throw new KeyNotFoundException();
        repository.Delete(entity);
        await repository.SaveAsync();
    }

    public virtual async Task<TDto> CreateAsync<TCreateDto, TDto>(TCreateDto dto)
        where TCreateDto : class
        where TDto : class
    {
        var entity = mapper.Map<TEntity>(dto);
        await repository.AddAsync(entity);
        await repository.SaveAsync();
        return mapper.Map<TDto>(entity);
    }
}