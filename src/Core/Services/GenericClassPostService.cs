using AutoMapper;
using Model.Dtos;
using Model.Entities;
using Repository.Repositories;

namespace Core.Services;

public class GenericClassPostService(IPostRepository postRepository, IMapper mapper)
    : GenericClassCrudService<Post, int, PostDto, PostDto, PostDto>(postRepository, mapper), IPostService
{
}