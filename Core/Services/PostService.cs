using AutoMapper;
using Model.Dtos;
using Model.Entities;
using Repository.Repositories;

namespace Core.Services;

public class PostService(IPostRepository postRepository, IMapper mapper)
    : CrudService<Post, int>(postRepository, mapper), IPostService
{
    public async Task<PostDto> CreateAsync(PostDto post)
    {
        return await base.CreateAsync<PostDto, PostDto>(post);
    }

    public async Task UpdateAsync(PostDto post)
    {
        await base.UpdateAsync(post.Id, post);
    }
}