using AutoMapper;

namespace Test.Mapping;

public class MappingTest(MappingFixture fixture) : IClassFixture<MappingFixture>
{
    private readonly IMapper _mapper = fixture.Mapper;

    [Fact]
    public void VerifyConfiguration()
    {
        _mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }
}