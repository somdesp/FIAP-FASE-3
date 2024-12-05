using FIAP.TECH.CORE.DOMAIN.Entities;
using FIAP.TECH.CORE.DOMAIN.Interfaces.Repositories;
using FIAP.TECH.INFRASTRUCTURE.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FIAP.TECH.INFRASTRUCTURE.Repositories;

public class ContactRepository : Repository<Contact>, IContactRepository
{
    private readonly AppDbContext _appDbContext;
    public ContactRepository(AppDbContext appDbContext) : base(appDbContext) =>
            _appDbContext = appDbContext;

    public async Task<IEnumerable<Contact>> GetByDdd(string ddd)
    {
        return await _appDbContext.Contacts
            .AsNoTracking()
            .Include(c => c.Region)
            .Where(c => c.DDD == ddd)
            .ToListAsync();
    }
}
