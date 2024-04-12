using AutoMapper;
using Model.Dtos;
using Model.ViewModels;

namespace Model.Mapping;

public class ViewModelDtoProfile : Profile
{
    public ViewModelDtoProfile()
    {
        CreateMap<PostViewModel, PostDto>().ReverseMap();
    }
}