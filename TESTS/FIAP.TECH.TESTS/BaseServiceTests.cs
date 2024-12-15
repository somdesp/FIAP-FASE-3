using AutoMapper;

namespace FIAP.TECH.TESTS;

public class BaseServiceTests<T>
{
    protected List<T>? _entityData;
    protected readonly IMapper _mapper;
    protected readonly CancellationToken _cancellationToken;

    public BaseServiceTests()
    {
        _mapper = MapperFactory.Create();
        _cancellationToken = CancellationToken.None;
    }
}
