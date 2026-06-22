namespace Application.Models.DTOs;

public class GetUserFromTokenDto
{
    public Guid Id { get; set; }
    public string Username { get; set; } = default!;
    public string FullName { get; set; } = default!;
    
    public bool IsAdmin { get; set; }
    public bool IsActive { get; set; }
}