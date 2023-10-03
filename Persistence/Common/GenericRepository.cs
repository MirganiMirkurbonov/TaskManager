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

    public async Task<T> Create(T entity,
        CancellationToken cancellationToken)
    {
        await _table.AddAsync(entity, cancellationToken);
        await _entityContext.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task Update(T entity,
        CancellationToken cancellationToken)
    {
        _table.Update(entity);
        await _entityContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteHard(T entity,
        CancellationToken cancellationToken)
    {
        _table.Remove(entity);
        await _entityContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<T?> GetById(long id,
        CancellationToken cancellationToken)
    {
        return await _table.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _entityContext.SaveChangesAsync(cancellationToken);
    }
    
    public IQueryable<T> QueryWithTracking()
    {
        return _table.AsNoTracking();
    }
    
    public IQueryable<T> QueryNoTracking()
    {
        return _table.AsNoTracking();
    }

    public async ValueTask<bool> Exists(Expression<Func<T, bool>> expression,
        CancellationToken cancellationToken)
    {
        return await _table.AnyAsync(expression, cancellationToken);
    }
}