using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

public class AuditLog : AuditableEntity
{
    public string TableName { get; init; } = default!;
    public EntityState EntityState { get; init; }
    public Guid RecordId { get; init; }
    public string NewValues { get; init; } = default!;
}
