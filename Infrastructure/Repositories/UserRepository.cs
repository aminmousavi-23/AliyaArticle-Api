using Application.Abstractions.Persistence;
using Application.Features.Auth.Command.Register;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository(AppDbContext context) : Repository<User>(context), IUserRepository
{
    public async Task<bool> IsExistsAsync(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        return await context.Users
            .AnyAsync(x =>
                    x.Username == request.Username ||
                    x.Email == request.Email ||
                    x.PhoneNumber == request.PhoneNumber, 
                cancellationToken);
    }

    public async Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken)
    {
        return await context.Users
            .FirstOrDefaultAsync(x => x.Username == username, cancellationToken);
    }
}