using Domain.Entities;

namespace Application.Abstractions.Persistence;

public interface IUserRepository : IRepository<User>
{
    Task<bool> IsExistsAsync(string phoneNumber, string? email, CancellationToken cancellationToken);
}