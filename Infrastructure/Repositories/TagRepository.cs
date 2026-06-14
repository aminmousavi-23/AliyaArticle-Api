using Application.Abstractions.Persistence;
using Application.Features.Auth.Command.Register;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TagRepository(AppDbContext context) : Repository<Tag>(context), ITagRepository
{
    public async Task<IList<Tag>> GetByIdsAsync(IList<Guid> ids, CancellationToken cancellationToken)
    {
        return await context.Tags
            .Where(t => ids.Contains(t.Id))
            .ToListAsync(cancellationToken);
    }
}