using Application.Features.Auth.Command.Register;

namespace Application.Abstractions.Persistence;

public interface IUserRepository : IRepository<User>
{
    Task<bool> ExistsAsync(RegisterUserCommand request, CancellationToken cancellationToken);
    Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken);
}