using Application.Abstractions.Persistence;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class UserRepository(AppDbContext context) : Repository<User>(context), IUserRepository
{
    
}