using Application.Features.Auth.Command.Register;
using Domain.Entities;

namespace Application.Abstractions.Persistence;

public interface ICategoryRepository : IRepository<Category>
{
    Task<bool> ExistsAsync(string slug, CancellationToken cancellationToken);
    Task<bool> HasArticlesAsync(Guid id, CancellationToken cancellationToken);
}