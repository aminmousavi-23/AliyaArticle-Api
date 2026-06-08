using System.Linq.Expressions;
using Application.Common.Querying.Filtering;
using Domain.Common;

namespace Application.Abstractions.Persistence;

public interface IRepository<T> where T : AuditableEntity
{
    Task<T?> GetByIdAsync<TKey>(TKey id, CancellationToken cancellationToken);
    Task<IList<T>> GetAllAsync(CancellationToken cancellationToken);
    Task<(IEnumerable<T> Items, long TotalCount)> GetPaginatedAsync(QueryOptions request, 
        Dictionary<string, Expression<Func<T, object>>>? mapping, CancellationToken cancellationToken);

    Task<T> AddAsync(T entity, CancellationToken cancellationToken);
    Task<IList<T>> AddRangeAsync(IList<T> entities, CancellationToken cancellationToken);
    Task<IList<T>> AddBulkAsync(IList<T> entities, CancellationToken cancellationToken);

    void Update(T entity);
    void UpdateRange(IList<T> entities);
    Task UpdateBulkAsync(IList<T> entities, CancellationToken cancellationToken);

    void Remove(T entity);
    void RemoveRange(IList<T> entities);
    Task RemoveBulkAsync(IList<T> entities, CancellationToken cancellationToken);
}