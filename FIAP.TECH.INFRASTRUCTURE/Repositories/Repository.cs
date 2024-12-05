using FIAP.TECH.CORE.DOMAIN.Entities;
using FIAP.TECH.CORE.DOMAIN.Interfaces.Repositories;
using FIAP.TECH.INFRASTRUCTURE.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FIAP.TECH.INFRASTRUCTURE.Repositories;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly AppDbContext _appDbContext;
    protected readonly DbSet<T> DbSet;

    public Repository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
        DbSet = appDbContext.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return await DbSet.ToListAsync();
    }

    public async Task<T?> GetById(int id)
    {
        return await DbSet.FindAsync(id);
    }

    public async Task<bool> Exists(Expression<Func<T, bool>> expression)
    {
        return await DbSet.AnyAsync(expression);
    }

    public async Task<T?> Search(Expression<Func<T, bool>> expression)
    {
        return await DbSet.FirstOrDefaultAsync(expression);
    }

    public async Task Create(T entity)
    {
        await DbSet.AddAsync(entity);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task Delete(T entity)
    {
        DbSet.Remove(entity);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task Update(T entity)
    {
        DbSet.Update(entity);
        await _appDbContext.SaveChangesAsync();
    }
}
