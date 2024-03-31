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
/// <typeparam name="TDto">Type of return DTO</typeparam>
/// <typeparam name="TCreateDto">Type of DTO to create entity</typeparam>
/// <typeparam name="TUpdateDto">Type of DTO to update entity</typeparam>
/// <remarks>
///     Uses generic methods because they might take a different shape of DTO. This is almost certainly the wrong way
///     to do this.
/// </remarks>
public class CrudService<TEntity, TId, TDto, TCreateDto, TUpdateDto>(
    ICrudRepository<TEntity, TId> repository,
    IMapper mapper)
    : IGenericClassCrudService<TEntity, TId, TDto, TCreateDto, TUpdateDto>
    where TEntity : class
    where TDto : class
    where TCreateDto : class
    where TUpdateDto : class
{
    public virtual async Task<TDto> CreateAsync(TCreateDto dto)
    {
        var entity = mapper.Map<TEntity>(dto);
        await repository.AddAsync(entity);
        await repository.SaveAsync();
        // Ideally we would return the created entity, but we don't have a way to get the ID.
        // Could have an IEntity interface with an ID property.
        return mapper.Map<TDto>(entity);
    }

    public virtual async Task<TDto> GetByIdAsync(TId id)
    {
        var entity = await repository.GetByIdAsync(id);
        if (entity == null) throw new KeyNotFoundException();
        return mapper.Map<TDto>(entity);
    }

    public virtual async Task<IEnumerable<TDto>> GetAllAsync()
    {
        var entities = await repository.GetAllAsync();
        return mapper.Map<List<TDto>>(entities);
    }

    // A little spooky if id arg and id property are not the same.
    // This is checked in controller.
    public virtual async Task UpdateAsync(TId id, TUpdateDto entity)
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
}