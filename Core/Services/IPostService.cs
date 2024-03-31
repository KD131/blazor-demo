using Model.Dtos;
using Model.Entities;

namespace Core.Services;

public interface IPostService : IGenericClassCrudService<Post, int, PostDto, PostDto, PostDto>
{
}