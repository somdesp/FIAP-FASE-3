using FIAP.TECH.CORE.DOMAIN.Entities;

namespace FIAP.TECH.CORE.DOMAIN.Interfaces.Repositories;

public interface IContactRepository : IRepository<Contact>
{
    Task<IEnumerable<Contact>> GetByDdd(string ddd);
}
