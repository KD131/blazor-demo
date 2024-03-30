using AutoMapper;
using Model.Dtos;
using Model.Entities;
using Repository.Repositories;

namespace Shared.Services;

public class PostService(IPostRepository postRepository, IMapper mapper) : IPostService
{
    public async Task<Post?> GetPostByIdAsync(int id)
    {
        var post = await postRepository.GetByIdAsync(id);
        if (post == null)
        {
            throw new KeyNotFoundException();
        }
        return post;
    }

    public async Task<IEnumerable<Post>> GetPostsAsync()
    {
        return await postRepository.GetAllAsync();
    }

    public async Task<Post> CreatePostAsync(PostDto post)
    {
        var entity = mapper.Map<Post>(post);
        await postRepository.AddAsync(entity);
        await postRepository.SaveAsync();
        return entity;
    }

    public async Task UpdatePostAsync(PostDto post)
    {
        var exists = await postRepository.GetByIdAsync(post.Id) != null;
        if (!exists)
        {
            throw new KeyNotFoundException();
        }
        var entity = mapper.Map<Post>(post);
        postRepository.Update(entity);
        await postRepository.SaveAsync();
    }

    public async Task DeletePostAsync(int id)
    {
        var entity = await postRepository.GetByIdAsync(id);
        if (entity == null)
        {
            throw new KeyNotFoundException();
        }
        postRepository.Delete(entity);
        await postRepository.SaveAsync();
    }
}