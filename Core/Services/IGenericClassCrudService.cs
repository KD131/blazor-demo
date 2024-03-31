namespace Core.Services;

public interface IGenericClassCrudService<TEntity, TId, TDto, TCreateDto, TUpdateDto>
    where TEntity : class
    where TDto : class
    where TCreateDto : class
    where TUpdateDto : class
{
    Task<TDto> CreateAsync(TCreateDto dto);
    Task<TDto> GetByIdAsync(TId id);
    Task<IEnumerable<TDto>> GetAllAsync();
    Task UpdateAsync(TId id, TUpdateDto entity);
    Task DeleteAsync(TId id);
}