using AutoMapper;
using Model.Mapping;
using Moq;
using Repository.Repositories;
using Shared.Services;

namespace Test.Services;

public class MockPostRepositoryFixture
{
    public readonly IMapper Mapper;
    public readonly Mock<IPostRepository> PostRepository;
    public readonly PostService PostService;

    public MockPostRepositoryFixture()
    {
        PostRepository = new Mock<IPostRepository>();
        Mapper = new MapperConfiguration(cfg => cfg.AddProfile<EntityDtoProfile>()).CreateMapper();
        PostService = new PostService(PostRepository.Object, Mapper);
    }
}