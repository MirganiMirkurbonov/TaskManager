using System.Linq.Expressions;
using Domain.Schemas.BaseEntity;

namespace Persistence.Common;

public interface IGenericRepository<T> where T : Entity
{
    Task<T> Create(T entity, CancellationToken cancellationToken);
    Task Update(T entity, CancellationToken cancellationToken);
    Task DeleteHard(T entity, CancellationToken cancellationToken);
    Task<T?> GetById(long id, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
    IQueryable<T> QueryWithTracking();
    IQueryable<T> QueryNoTracking();
    ValueTask<bool> Exists(Expression<Func<T, bool>> expression, CancellationToken cancellationToken);
}