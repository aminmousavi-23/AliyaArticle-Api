namespace Application.Common.Querying.Paging;

public static class QueryPaginationBuilder
{
    public static IQueryable<T> ApplyPaging<T>(IQueryable<T> query, int pageNumber, int pageSize)
    {
        return query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);
    }
}