using Application.Abstractions.Persistence;
using Application.Features.Auth.Command.Register;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CategoryRepository(AppDbContext context) : Repository<Category>(context), ICategoryRepository
{
    public async Task<bool> ExistsAsync(string slug, CancellationToken cancellationToken)
    {
        return await context.Categories
            .AnyAsync(a => a.Slug == slug, cancellationToken);
    }
    
    public async Task<bool> HasArticlesAsync(Guid id, CancellationToken cancellationToken)
    {
        return await context.Articles
            .AnyAsync(a => a.CategoryId == id, cancellationToken);
    }
}