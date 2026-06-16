using System.Linq.Expressions;
using Application.Features.Article.Queries.GetPaginated;
using Domain.Entities;

namespace Application.Abstractions.Persistence;

public interface IArticleRepository : IRepository<Article>
{
    Task<(IEnumerable<Article> Items, long TotalCount)> GetPaginatedAsync(GetArticlePaginatedQuery request, 
        Dictionary<string, Expression<Func<Article, object>>>? mapping, CancellationToken cancellationToken);
    Task<Article?> GetByIdWithDetail(Guid id, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(string slug, CancellationToken cancellationToken);
}