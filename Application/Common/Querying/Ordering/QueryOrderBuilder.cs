using System.Linq.Expressions;
using Application.Common.Querying.Filtering;

namespace Application.Common.Querying.Ordering;

public static class QueryOrderBuilder
{
    public static IQueryable<TEntity> ApplyOrdering<TEntity>(
        IQueryable<TEntity> query,
        FilterDto? filter,
        Dictionary<string, Expression<Func<TEntity, object>>> mapping)
    {
        if (filter?.OrderBy == null)
            return query;

        if (!mapping.TryGetValue(filter.OrderBy, out var expression))
            return query;

        return filter.IsAscending
            ? query.OrderBy(expression)
            : query.OrderByDescending(expression);
    }
}