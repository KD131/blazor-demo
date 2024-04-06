using Model.Dtos;
using Model.Entities;

namespace Core.Services;

// Can either extend the generic service interface, or add its own methods.
// Implementation can then also choose to extend the generic, or compose the generic method service.
// Or both.
public interface IPostService : IGenericClassCrudService<Post, int, PostDto, PostDto, PostDto>
{
}