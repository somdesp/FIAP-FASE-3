using AutoMapper;
using FIAP.TECH.CORE.APPLICATION.Settings.AutoMapper;

namespace FIAP.TECH.TESTS;

public class MapperFactory
{
    public static IMapper Create()
    {
        var configuration = new MapperConfiguration(cfg =>
            cfg.AddProfile<MappingProfile>());
        return new Mapper(configuration);
    }
}
