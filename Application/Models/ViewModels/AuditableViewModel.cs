namespace Application.Models.ViewModels;

public class AuditableViewModel : BaseViewModel
{
    public string CreatedBy { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public string? LastModifiedBy { get; set;}
    public DateTime? LastModifiedAt { get; set;}
}