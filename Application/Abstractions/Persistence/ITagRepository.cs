using Application.Features.Auth.Command.Register;
using Domain.Entities;

namespace Application.Abstractions.Persistence;

public interface ITagRepository : IRepository<Tag>
{
    Task<bool> ExistsAsync(string slug, CancellationToken cancellationToken);
    Task<bool> HasArticlesAsync(Guid id, CancellationToken cancellationToken);
}