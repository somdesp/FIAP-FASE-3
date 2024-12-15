using FIAP.TECH.CORE.DOMAIN.Entities;
using FIAP.TECH.CORE.DOMAIN.Interfaces.Repositories;
using System.Linq.Expressions;

namespace FIAP.TECH.TESTS.TestDoubles.Fakes;

public class FakeRepository<T> : List<T>, IRepository<T> where T : BaseEntity
{
    public FakeRepository(IEnumerable<T> entityCollection)
        => AddRange(entityCollection);

    public Task Create(T entity)
    {
        Add(entity);
        return Task.CompletedTask;
    }

    public Task Delete(T entity)
    {
        Remove(entity);
        return Task.CompletedTask;
    }

    public Task<bool> Exists(Expression<Func<T, bool>> expression)
    {
        return Task.FromResult(this.Any(expression.Compile()));
    }

    public Task<IEnumerable<T>> GetAll()
    {
        return Task.FromResult(this.AsEnumerable());
    }

    public Task<T?> GetById(int id)
    {
        return Task.FromResult(this.FirstOrDefault(e => e.Id == id));
    }

    public Task<T?> Search(Expression<Func<T, bool>> expression)
    {
        return Task.FromResult(this.FirstOrDefault(expression.Compile()));
    }

    public Task Update(T entity)
    {
        return Task.FromResult(Update(entity));
    }
}
