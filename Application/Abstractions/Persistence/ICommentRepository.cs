using System.Linq.Expressions;
using Application.Features.Comment.Queries.GetPaginated;

namespace Application.Abstractions.Persistence;

public interface ICommentRepository : IRepository<Comment>
{
    Task<(IEnumerable<Comment> Items, long TotalCount)> GetPaginatedAsync(
        GetCommentPaginatedQuery request,
        Dictionary<string, Expression<Func<Comment, object>>>? mapping, CancellationToken cancellationToken);
}