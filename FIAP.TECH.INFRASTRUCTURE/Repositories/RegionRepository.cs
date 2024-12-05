using FIAP.TECH.CORE.DOMAIN.Entities;
using FIAP.TECH.CORE.DOMAIN.Interfaces.Repositories;
using FIAP.TECH.INFRASTRUCTURE.Contexts;

namespace FIAP.TECH.INFRASTRUCTURE.Repositories;

public class RegionRepository : Repository<Region>, IRegionRepository
{
    public RegionRepository(AppDbContext appDbContext) : base(appDbContext) { }
}
