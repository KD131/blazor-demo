using Model.Dtos;
using Model.Entities;

namespace Core.Services;

public interface IPostService : ICrudService<Post, int>
{
    Task<PostDto> CreateAsync(PostDto post);
    Task UpdateAsync(PostDto post);
}