using AutoMapper;
using Core.Services;
using Model.Mapping;
using Moq;
using Repository.Repositories;

namespace Test.Services;

public class MockPostRepositoryFixture
{
    public readonly IMapper Mapper;
    public readonly Mock<IPostRepository> PostRepository;
    public readonly IPostService PostService;

    public MockPostRepositoryFixture()
    {
        PostRepository = new Mock<IPostRepository>();
        Mapper = new MapperConfiguration(cfg => cfg.AddProfile<EntityDtoProfile>()).CreateMapper();
        PostService = new GenericClassPostService(PostRepository.Object, Mapper);
    }
}