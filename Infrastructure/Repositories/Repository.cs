using System.Linq.Expressions;
using Application.Abstractions.Persistence;
using Application.Common.Querying.Filtering;
using Application.Common.Querying.Ordering;
using Application.Common.Querying.Paging;
using Domain.Common;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class Repository<T>(DbContext context) : IRepository<T>
    where T : AuditableEntity
{
    public virtual async Task<T?> GetByIdAsync<TKey>(TKey id, CancellationToken cancellationToken)
    {
        return await context.Set<T>().FindAsync([id], cancellationToken);
    }

    public virtual async Task<IList<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await context.Set<T>()
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedDate)
            .ToListAsync(cancellationToken);
    }

    public virtual async Task<(IList<T> Items, long TotalCount)> GetPaginatedAsync(QueryOptionsDto request,
        Dictionary<string, Expression<Func<T, object>>> mapping, CancellationToken cancellationToken)
    {
        var query = context.Set<T>()
            .AsNoTracking()
            .AsQueryable();

        query = QueryFilterBuilder.ApplyFiltering(query, request.Filter, mapping);

        var totalCount = await query.LongCountAsync(cancellationToken);

        query = QueryOrderBuilder.ApplyOrdering(query, request.Filter, mapping);

        query = QueryPaginationBuilder.ApplyPaging(query, request.PageNumber, request.PageSize);

        var items = await query.ToListAsync(cancellationToken);

        return (items, totalCount);
    }


    public virtual async Task<T> AddAsync(T entity, CancellationToken cancellationToken)
    {
        await context.Set<T>()
            .AddAsync(entity, cancellationToken);
        return entity;
    }

    public virtual async Task<IList<T>> AddRangeAsync(IList<T> entities, CancellationToken cancellationToken)
    {
        await context.Set<T>().AddRangeAsync(entities, cancellationToken);
        return entities;
    }

    public virtual async Task<IList<T>> AddBulkAsync(IList<T> entities, CancellationToken cancellationToken)
    {
        await context.BulkInsertAsync(entities, cancellationToken: cancellationToken);
        return entities;
    }

    public virtual void Update(T entity)
    {
        context.Entry(entity).State = EntityState.Modified;
    }

    public virtual void UpdateRange(IList<T> entities)
    {
        context.Set<T>().UpdateRange(entities);
    }

    public virtual async Task UpdateBulkAsync(IList<T> entities, CancellationToken cancellationToken)
    {
        await context.BulkUpdateAsync(entities, cancellationToken: cancellationToken);
    }

    public virtual void Remove(T entity)
    {
        context.Set<T>().Remove(entity);
    }

    public virtual void RemoveRange(IList<T> entities)
    {
        context.Set<T>().RemoveRange(entities);
    }

    public virtual async Task RemoveBulkAsync(IList<T> entities, CancellationToken cancellationToken)
    {
        await context.BulkDeleteAsync(entities, cancellationToken: cancellationToken);
    }
}