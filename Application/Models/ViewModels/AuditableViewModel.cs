namespace Application.Models.ViewModels;

public class AuditableViewModel : BaseViewModel
{
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid? LastModifiedBy { get; set;}
    public DateTime? LastModifiedAt { get; set;}

    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
}