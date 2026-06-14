using Application.Abstractions.Persistence;
using Application.Features.Auth.Command.Register;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TagRepository(AppDbContext context) : Repository<Tag>(context), ITagRepository
{
    public async Task<bool> ExistsAsync(string slug, CancellationToken cancellationToken)
    {
        return await context.Tags
            .AnyAsync(a => a.Slug == slug, cancellationToken);
    }
    
    public async Task<bool> HasArticlesAsync(Guid id, CancellationToken cancellationToken)
    {
        return await context.Articles
            .AnyAsync(a => a.Tags.Any(t => t.Id == id), cancellationToken);
    }
}