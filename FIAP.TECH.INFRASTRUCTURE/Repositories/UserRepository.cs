using FIAP.TECH.CORE.DOMAIN.Entities;
using FIAP.TECH.CORE.DOMAIN.Interfaces.Repositories;
using FIAP.TECH.INFRASTRUCTURE.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FIAP.TECH.INFRASTRUCTURE.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    private readonly AppDbContext _appDbContext;
    public UserRepository(AppDbContext appDbContext) : base(appDbContext) =>
            _appDbContext = appDbContext;

    public async Task<User?> Authenticate(string email, string password)
    {
        return await _appDbContext.Users.AsNoTracking().SingleOrDefaultAsync(x => x.Email == email && x.Password == password && x.IsActive);
    }
}
