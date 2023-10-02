using System.Linq.Expressions;
using Domain.Schemas.BaseEntity;
using Microsoft.EntityFrameworkCore;
using Persistence.DbContext;

namespace Persistence.Common;

internal class GenericRepository<T> : IGenericRepository<T> where T : Entity
{
    private readonly EntityContext _entityContext;
    private readonly DbSet<T> _table;

    public GenericRepository(EntityContext entityContext)
    {
        _entityContext = entityContext;
        _table = _entityContext.Set<T>();
    }

    public async Task<T> Create(T entity)
    {
        await _table.AddAsync(entity);
        await _entityContext.SaveChangesAsync();
        return entity;
    }

    public async void Update(T entity)
    {
        _table.Update(entity);
        await _entityContext.SaveChangesAsync();
    }

    public async void Delete(T entity)
    {
        _table.Remove(entity);
        await _entityContext.SaveChangesAsync();
    }

    public async Task<T?> GetById(long id)
    {
        return await _table.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task SaveChangesAsync()
    {
        await _entityContext.SaveChangesAsync();
    }
    
    public IQueryable<T> Query()
    {
        return _table.AsNoTracking();
    }

    public async ValueTask<bool> Exists(Expression<Func<T, bool>> expression)
    {
        return await _table.AnyAsync(expression);
    }
}