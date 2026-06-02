namespace Domain.Common;

public class AuditableEntity
{
    public Guid Id { get; set; } =  Guid.NewGuid();

    public string CreatedBy { get; set; } = default!;
    public DateTime CreatedDate { get; set; }
    public string? LastModifiedBy { get; set;}
    public DateTime? LastModifiedDate { get; set;}

    public bool IsDeleted { get; set; } = false;
}