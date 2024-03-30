using Model.Dtos;
using Model.Entities;

namespace Shared.Services;

public interface IPostService
{
    Task<Post?> GetPostByIdAsync(int id);
    Task<IEnumerable<Post>> GetPostsAsync();
    Task<Post> CreatePostAsync(PostDto post);
    Task UpdatePostAsync(PostDto post);
    Task DeletePostAsync(int id);
}