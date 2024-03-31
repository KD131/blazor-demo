namespace Core.Services;

public interface ICrudService<TEntity, TId> where TEntity : class
{
    Task<TDto> CreateAsync<TCreateDto, TDto>(TCreateDto dto)
        where TCreateDto : class
        where TDto : class;

    Task<TEntity> GetByIdAsync(TId id);
    Task<IEnumerable<TEntity>> GetAllAsync();

    Task UpdateAsync<TUpdateDto>(TId id, TUpdateDto entity)
        where TUpdateDto : class;

    Task DeleteAsync(TId id);
}