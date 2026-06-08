using Application.Abstractions.Persistence;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository(AppDbContext context) : Repository<User>(context), IUserRepository
{
    public async Task<bool> IsExistsAsync(string phoneNumber, string? email, CancellationToken cancellationToken)
    {
        return await context.Users
            .AnyAsync(x =>
                    x.Email == email ||
                    x.PhoneNumber == phoneNumber, 
                cancellationToken);
    }
}