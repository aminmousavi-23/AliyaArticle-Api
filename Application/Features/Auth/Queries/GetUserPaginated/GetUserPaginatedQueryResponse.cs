using Application.Models.ViewModels;

namespace Application.Features.Auth.Queries.GetUserPaginated;

public class GetUserPaginatedQueryResponse : AuditableViewModel
{
    public string Username { get; set; } = default!;
    public string FullName { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string? Email { get; set; }
    
    public bool IsAdmin { get; set; }
}