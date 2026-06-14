using Application.Features.Auth.Command.Register;
using Domain.Entities;

namespace Application.Abstractions.Persistence;

public interface IArticleRepository : IRepository<Article>
{
    Task<Article?> GetByIdWithDetail(Guid id, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(string slug, CancellationToken cancellationToken);
}