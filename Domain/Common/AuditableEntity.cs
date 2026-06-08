namespace Domain.Common;

public class AuditableEntity
{
    public Guid Id { get; set; } =  Guid.NewGuid();

    public string CreatedBy { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public string? LastModifiedBy { get; set;}
    public DateTime? LastModifiedAt { get; set;}

    public bool IsActive { get; set; } = true;
    public bool IsDeleted { get; set; } = false;
}