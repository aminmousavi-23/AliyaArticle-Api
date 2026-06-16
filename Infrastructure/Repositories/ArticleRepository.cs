using Application.Abstractions.Persistence;
using Application.Features.Auth.Command.Register;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ArticleRepository(AppDbContext context) : Repository<Article>(context), IArticleRepository
{
    public async Task<Article?> GetByIdWithDetail(Guid id, CancellationToken cancellationToken)
    {
        return await context.Articles
            .Where(a => a.Id == id)
            .Include(a => a.Tags)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(string slug, CancellationToken cancellationToken)
    {
        return await context.Articles
            .AnyAsync(a => a.Slug == slug, cancellationToken);
    }
}