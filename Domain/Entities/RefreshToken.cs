using Domain.Common;

namespace Domain.Entities;

public class RefreshToken : AuditableEntity
{
    public string TokenHash { get; set; } = default!;
    public DateTime ExpiresAt { get; set; }
    public bool IsRevoked { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; } = default!;
}