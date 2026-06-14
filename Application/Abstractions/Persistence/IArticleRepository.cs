using Application.Features.Auth.Command.Register;
using Domain.Entities;

namespace Application.Abstractions.Persistence;

public interface IArticleRepository : IRepository<Article>
{
    Task<bool> ExistsAsync(string slug, CancellationToken cancellationToken);
}