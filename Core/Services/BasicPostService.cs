using AutoMapper;
using Model.Dtos;
using Model.Entities;
using Repository.Repositories;

namespace Core.Services;

public class BasicPostService(ICrudRepository<Post, int> repository, IMapper mapper) : IPostService
{
    public async Task<PostDto> CreateAsync(PostDto dto)
    {
        var entity = mapper.Map<Post>(dto);
        await repository.AddAsync(entity);
        await repository.SaveAsync();
        // Get fresh from repo to fill out data that might have been generated or omitted in the create DTO.
        var createdEntity = await repository.GetByIdAsync(entity.Id);
        return mapper.Map<PostDto>(createdEntity);
    }

    public async Task<PostDto> GetByIdAsync(int id)
    {
        var entity = await repository.GetByIdAsync(id);
        if (entity == null) throw new KeyNotFoundException();
        return mapper.Map<PostDto>(entity);
    }

    public async Task<IEnumerable<PostDto>> GetAllAsync()
    {
        var entities = await repository.GetAllAsync();
        return mapper.Map<List<PostDto>>(entities);
    }

    // A little spooky if id arg and id property are not the same.
    // This is checked in controller.
    public async Task UpdateAsync(int id, PostDto entity)
    {
        var exists = await repository.GetByIdAsync(id) != null;
        if (!exists) throw new KeyNotFoundException();
        var updatedEntity = mapper.Map<Post>(entity);
        repository.Update(updatedEntity);
        await repository.SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await repository.GetByIdAsync(id);
        if (entity == null) throw new KeyNotFoundException();
        repository.Delete(entity);
        await repository.SaveAsync();
    }
}