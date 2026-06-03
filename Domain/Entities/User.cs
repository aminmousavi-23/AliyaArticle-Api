using Domain.Common;

namespace Domain.Entities;

public class User : AuditableEntity
{
    public string FullName { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string? Email { get; set; }
    
    public string HashedPassword { get; set; } = default!;
    
    public bool IsAdmin { get; set; }
}
//TODO: refresh-token-redis => refresh_token:{tokenId}