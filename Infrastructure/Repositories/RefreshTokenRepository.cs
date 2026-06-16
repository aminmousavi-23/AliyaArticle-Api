using System.Linq.Expressions;
using Application.Abstractions.Persistence;
using Application.Common.Querying.Filtering;
using Application.Common.Querying.Ordering;
using Application.Common.Querying.Paging;
using Application.Features.Comment.Queries.GetPaginated;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class RefreshTokenRepository(AppDbContext context) : Repository<RefreshToken>(context), IRefreshTokenRepository
{
    public async Task<RefreshToken?> GetByHashAsync(string hash, CancellationToken cancellationToken)
    {
        return await context.RefreshTokens
            .Where(x => x.TokenHash == hash)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task RevokeByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        await context.RefreshTokens
            .Where(x => x.UserId == userId && x.IsRevoked == false)
            .ExecuteUpdateAsync(x => x
                    .SetProperty(t => t.IsRevoked, true), cancellationToken);
    }
}