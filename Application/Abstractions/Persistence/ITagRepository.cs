using Domain.Entities;

namespace Application.Abstractions.Persistence;

public interface ITagRepository : IRepository<Tag>
{
    Task<List<Tag>> GetByIdsAsync(List<Guid> ids, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(string slug, CancellationToken cancellationToken);
    Task<bool> HasArticlesAsync(Guid id, CancellationToken cancellationToken);
}