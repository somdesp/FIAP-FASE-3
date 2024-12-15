using FIAP.TECH.CORE.DOMAIN.Entities;
using FIAP.TECH.CORE.DOMAIN.Interfaces.Repositories;

namespace FIAP.TECH.TESTS.TestDoubles.Fakes;

public class FakeContactRepository : FakeRepository<Contact>, IContactRepository
{
    public FakeContactRepository(IEnumerable<Contact> entityCollection) 
        : base(entityCollection) { }

    public Task<IEnumerable<Contact>> GetByDdd(string ddd)
    {
        return Task.FromResult(this.Where(c => c.DDD == ddd));
    }
}
