using System.Linq.Expressions;
using Application.Common.Helpers;

namespace Application.Common.Querying.Filtering;

public static class QueryFilterBuilder
{
    public static IQueryable<TEntity> ApplyFiltering<TEntity>(
        IQueryable<TEntity> query,
        FilterDto? filter,
        Dictionary<string, Expression<Func<TEntity, object>>> mapping)
    {
        if (filter == null) 
            return query;

        var parameter = Expression.Parameter(typeof(TEntity), "x");
        var body = BuildGroupExpression(filter, parameter, mapping);

        if (body == null) 
            return query;

        var lambda = Expression.Lambda<Func<TEntity, bool>>(body, parameter);
        return query.Where(lambda);
    }

    private static Expression? BuildGroupExpression<TEntity>(
        FilterDto group,
        ParameterExpression parameter,
        Dictionary<string, Expression<Func<TEntity, object>>> mapping)
    {
        Expression? combined = null;

        // 1. Process leaf items (e.g., IsActive == true)
        foreach (var item in group.Items)
        {
            var itemExpr = BuildItemExpression(item, parameter, mapping);
            combined = Combine(combined, itemExpr, group.IsAnd);
        }

        // 2. Process nested groups (e.g., (Name == 'A' OR Name == 'B'))
        foreach (var subGroup in group.Groups)
        {
            var subExpr = BuildGroupExpression(subGroup, parameter, mapping);
            combined = Combine(combined, subExpr, group.IsAnd);
        }

        return combined;
    }

    private static Expression? BuildItemExpression<TEntity>(
        FilterItemDto item,
        ParameterExpression parameter,
        Dictionary<string, Expression<Func<TEntity, object>>> mapping)
    {
        if (!mapping.TryGetValue(item.Field, out var mapExpression))
            return null;

        // Ensure the parameter (x) in the map matches the parameter (x) in our final query
        var body = ReplaceParameter(mapExpression.Body, mapExpression.Parameters[0], parameter);

        // Strip out 'object' conversion from the mapping expression if it exists
        if (body is UnaryExpression { NodeType: ExpressionType.Convert } unary)
            body = unary.Operand;

        return item.Operation switch
        {
            FilterOperation.Equal => Expression.Equal(body, CreateConstant(item.Value, body.Type)),
            FilterOperation.NotEqual => Expression.NotEqual(body, CreateConstant(item.Value, body.Type)),
            FilterOperation.GreaterThan => Expression.GreaterThan(body, CreateConstant(item.Value, body.Type)),
            FilterOperation.LessThan => Expression.LessThan(body, CreateConstant(item.Value, body.Type)),
            FilterOperation.Contains => HandleContains(body, item.Value),
            FilterOperation.Include => BuildEnumerableContains(body, item.Value),
            FilterOperation.Exclude => Expression.Not(BuildEnumerableContains(body, item.Value)),
            _ => throw new NotSupportedException($"Operation {item.Operation} is not supported.")
        };
    }

    private static Expression? Combine(Expression? left, Expression? right, bool isAnd)
    {
        if (left == null)
            return right;
        if (right == null)
            return left;
        return isAnd ? Expression.AndAlso(left, right) : Expression.OrElse(left, right);
    }

    private static ConstantExpression CreateConstant(object? value, Type type)
    {
        // Important: ObjectHelper should handle JsonElement -> specific type conversion
        return Expression.Constant(ObjectHelper.ConvertValue(value, type), type);
    }

    private static Expression HandleContains(Expression body, object? value)
    {
        if (body.Type == typeof(string))
        {
            var method = typeof(string).GetMethod("Contains", [typeof(string)]);
            return Expression.Call(body, method!, Expression.Constant(value?.ToString()));
        }

        throw new NotSupportedException("Contains is only supported for string types.");
    }

    private static Expression BuildEnumerableContains(Expression body, object? value)
    {
        var typedList = ObjectHelper.ConvertEnumerable(value, body.Type);
        var listType = typeof(IEnumerable<>).MakeGenericType(body.Type);
        return Expression.Call(typeof(Enumerable), nameof(Enumerable.Contains), [body.Type],
            Expression.Constant(typedList, listType), body);
    }

    private static Expression ReplaceParameter(Expression body, ParameterExpression source, ParameterExpression target)
    {
        return new ReplaceExpressionVisitor(source, target).Visit(body)!;
    }

    private class ReplaceExpressionVisitor(ParameterExpression source, ParameterExpression target) : ExpressionVisitor
    {
        protected override Expression VisitParameter(ParameterExpression node)
            => node == source ? target : base.VisitParameter(node);
    }
}