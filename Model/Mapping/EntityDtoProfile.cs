using AutoMapper;
using Model.Dtos;
using Model.Entities;

namespace Model.Mapping;

public class EntityDtoProfile : Profile
{
    public EntityDtoProfile()
    {
        CreateMap<Post, PostDto>().ReverseMap();
    }
}