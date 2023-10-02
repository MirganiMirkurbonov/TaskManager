using System.Linq.Expressions;
using Domain.Schemas.BaseEntity;

namespace Persistence.Common;

public interface IGenericRepository<T> where T : Entity
{
    Task<T> Create(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task<T?> GetById(long id);
    Task SaveChangesAsync();
    IQueryable<T> Query();
    ValueTask<bool> Exists(Expression<Func<T, bool>> expression);
}