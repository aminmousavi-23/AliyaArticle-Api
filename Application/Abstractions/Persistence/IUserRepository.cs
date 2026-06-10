using Application.Features.Auth.Command.Register;
using Domain.Entities;

namespace Application.Abstractions.Persistence;

public interface IUserRepository : IRepository<User>
{
    Task<bool> IsExistsAsync(RegisterUserCommand request, CancellationToken cancellationToken);
    Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken);
}