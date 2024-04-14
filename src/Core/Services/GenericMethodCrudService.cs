using AutoMapper;
using Repository.Repositories;

namespace Core.Services;

/// <summary>
///     Uses generic methods because the shape of the object a method takes and returns is not set in stone.
/// </summary>
/// <param name="repository"></param>
/// <param name="mapper"></param>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TId"></typeparam>
/// <remarks>
///     Use composition over inheritance to restrict clients from accessing generic methods and using types that don't
///     make sense. Like a facade.
/// </remarks>
public class GenericMethodCrudService<TEntity, TId>(ICrudRepository<TEntity, TId> repository, IMapper mapper)
    : IGenericMethodCrudService<TEntity, TId> where TEntity : class
{
    public virtual async Task<TDto> CreateAsync<TCreateDto, TDto>(TCreateDto dto)
        where TCreateDto : class
        where TDto : class
    {
        var entity = mapper.Map<TEntity>(dto);
        await repository.AddAsync(entity);
        await repository.SaveAsync();
        // Ideally we would return the created entity, but we don't have a way to get the ID.
        // Could have an IEntity interface with an ID property.
        return mapper.Map<TDto>(entity);
    }

    public virtual async Task<TDto> GetByIdAsync<TDto>(TId id)
        where TDto : class
    {
        var entity = await repository.GetByIdAsync(id);
        if (entity == null) throw new KeyNotFoundException();
        return mapper.Map<TDto>(entity);
    }

    public virtual async Task DeleteAsync(TId id)
    {
        var entity = await repository.GetByIdAsync(id);
        if (entity == null) throw new KeyNotFoundException();
        repository.Delete(entity);
        await repository.SaveAsync();
    }

    public virtual async Task<IEnumerable<TDto>> GetAllAsync<TDto>()
        where TDto : class
    {
        var entities = await repository.GetAllAsync();
        return mapper.Map<List<TDto>>(entities);
    }

    // A little spooky if id arg and id property are not the same.
    // This is checked in controller.
    public virtual async Task UpdateAsync<TUpdateDto>(TId id, TUpdateDto entity)
        where TUpdateDto : class
    {
        var exists = await repository.GetByIdAsync(id) != null;
        if (!exists) throw new KeyNotFoundException();
        var updatedEntity = mapper.Map<TEntity>(entity);
        repository.Update(updatedEntity);
        await repository.SaveAsync();
    }
}