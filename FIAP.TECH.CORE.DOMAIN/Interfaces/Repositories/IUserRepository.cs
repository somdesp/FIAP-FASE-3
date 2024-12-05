using FIAP.TECH.CORE.DOMAIN.Entities;

namespace FIAP.TECH.CORE.DOMAIN.Interfaces.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> Authenticate(string email, string password);
}
