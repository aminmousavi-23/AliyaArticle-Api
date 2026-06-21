
namespace Application.Abstractions.Persistence;

public interface IRefreshTokenRepository : IRepository<RefreshToken>
{
    Task<RefreshToken?> GetByHashAsync(string hash, CancellationToken cancellationToken);
    Task RevokeByUserIdAsync(Guid userId, CancellationToken cancellationToken);
}