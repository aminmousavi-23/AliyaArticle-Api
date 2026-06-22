namespace Domain.Common;

public class AuditableEntity
{
    public Guid Id { get; set; } =  Guid.NewGuid();

    public Guid CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid? LastModifiedBy { get; set;}
    public DateTime? LastModifiedAt { get; set;}

    public bool IsActive { get; set; } = true;
    public bool IsDeleted { get; set; } = false;
}