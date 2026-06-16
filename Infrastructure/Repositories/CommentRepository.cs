using System.Linq.Expressions;
using Application.Abstractions.Persistence;
using Application.Common.Querying.Filtering;
using Application.Common.Querying.Ordering;
using Application.Common.Querying.Paging;
using Application.Features.Comment.Queries.GetPaginated;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CommentRepository(AppDbContext context) : Repository<Comment>(context), ICommentRepository
{
    public async Task<(IEnumerable<Comment> Items, long TotalCount)> GetPaginatedAsync(
        GetCommentPaginatedQuery request,
        Dictionary<string, Expression<Func<Comment, object>>>? mapping, CancellationToken cancellationToken)
    {
        var query = context.Comments
            .AsNoTracking()
            .Where(x => x.ArticleId == request.ArticleId)
            .AsQueryable();

        query = QueryFilterBuilder.ApplyFiltering(query, request.Filter, mapping);

        var totalCount = await query.LongCountAsync(cancellationToken);

        query = QueryOrderBuilder.ApplyOrdering(query, request.Filter, mapping);

        query = QueryPaginationBuilder.ApplyPaging(query, request.PageNumber, request.PageSize);

        var items = await query.ToListAsync(cancellationToken);

        return (items, totalCount);
    }
}