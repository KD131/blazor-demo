using Model.Dtos;

namespace Core.Services;

/// <summary>
/// Basically a facade for the generic service, to make it easier to use, and restrict access to generic methods.
/// </summary>
/// <param name="service">Backing service that uses generic methods.</param>
public class GenericMethodPostService(IGenericMethodCrudService<PostDto, int> service) : IPostService
{
    public async Task<PostDto> CreateAsync(PostDto dto)
    {
        return await service.CreateAsync<PostDto, PostDto>(dto);
    }

    public async Task<PostDto> GetByIdAsync(int id)
    {
        return await service.GetByIdAsync<PostDto>(id);
    }

    public async Task<IEnumerable<PostDto>> GetAllAsync()
    {
        return await service.GetAllAsync<PostDto>();
    }

    public async Task UpdateAsync(int id, PostDto entity)
    {
        await service.UpdateAsync(id, entity);
    }

    public async Task DeleteAsync(int id)
    {
        await service.DeleteAsync(id);
    }
}