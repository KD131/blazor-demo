namespace Core.Services;

public interface IGenericMethodCrudService<TEntity, TId> where TEntity : class
{
    Task<TDto> CreateAsync<TCreateDto, TDto>(TCreateDto dto)
        where TCreateDto : class
        where TDto : class;

    Task<TDto> GetByIdAsync<TDto>(TId id)
        where TDto : class;

    Task<IEnumerable<TDto>> GetAllAsync<TDto>()
        where TDto : class;

    Task UpdateAsync<TUpdateDto>(TId id, TUpdateDto entity)
        where TUpdateDto : class;

    Task DeleteAsync(TId id);
}