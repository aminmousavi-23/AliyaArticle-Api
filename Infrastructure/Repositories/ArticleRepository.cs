using Application.Abstractions.Persistence;
using Application.Common.Querying.Filtering;
using Application.Common.Querying.Ordering;
using Application.Common.Querying.Paging;
using Application.Features.Article.Queries.GetPaginated;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class ArticleRepository(AppDbContext context) : Repository<Article>(context), IArticleRepository
{
    public async Task<(IEnumerable<Article> Items, long TotalCount)> GetPaginatedAsync(
        GetArticlePaginatedQuery request,
        Dictionary<string, Expression<Func<Article, object>>>? mapping, CancellationToken cancellationToken)
    {
        var query = context.Articles
            .AsNoTracking()
            .Include(a => a.Category)
            .AsQueryable();

        query = QueryFilterBuilder.ApplyFiltering(query, request.Filter, mapping);

        var totalCount = await query.LongCountAsync(cancellationToken);

        query = QueryOrderBuilder.ApplyOrdering(query, request.Filter, mapping);

        query = QueryPaginationBuilder.ApplyPaging(query, request.PageNumber, request.PageSize);

        var items = await query.ToListAsync(cancellationToken);

        return (items, totalCount);
    }

    public async Task<Article?> GetByIdWithDetail(Guid id, CancellationToken cancellationToken)
    {
        return await context.Articles
            .Where(a => a.Id == id)
            .Include(a => a.Category)
            .Include(a => a.Tags)
            .Include(a => a.Blocks)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(string slug, CancellationToken cancellationToken)
    {
        return await context.Articles
            .AnyAsync(a => a.Slug == slug, cancellationToken);
    }
}