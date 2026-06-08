using System.Linq.Expressions;
using Application.Common.Querying.Filtering;
using Domain.Common;

namespace Application.Common.Querying.Ordering;

public static class QueryOrderBuilder
{
    public static IQueryable<TEntity> ApplyOrdering<TEntity>(
        IQueryable<TEntity> query,
        FilterDto? filter,
        Dictionary<string, Expression<Func<TEntity, object>>>? mapping) where TEntity : AuditableEntity
    {
        if (string.IsNullOrEmpty(filter?.OrderBy) || 
            mapping == null ||
            mapping.TryGetValue(filter.OrderBy, out var expression) != true)
        {
            return query.OrderByDescending(x => x.CreatedAt);
        }

        return filter.IsAscending
            ? query.OrderBy(expression)
            : query.OrderByDescending(expression);
    }
}