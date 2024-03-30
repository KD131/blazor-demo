using AutoMapper;
using Model.Mapping;

namespace Test.Mapping;

public class MappingFixture
{
    public readonly IMapper Mapper;
    
    public MappingFixture()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddMaps(typeof(EntityDtoProfile).Assembly);
        });
        Mapper = configuration.CreateMapper();
    }
}