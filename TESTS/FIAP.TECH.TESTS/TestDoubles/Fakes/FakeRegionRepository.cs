using FIAP.TECH.CORE.DOMAIN.Entities;
using FIAP.TECH.CORE.DOMAIN.Interfaces.Repositories;

namespace FIAP.TECH.TESTS.TestDoubles.Fakes;

public class FakeRegionRepository : FakeRepository<Region>, IRegionRepository
{
    public FakeRegionRepository(IEnumerable<Region> entityCollection) 
        : base(entityCollection) { }
}
