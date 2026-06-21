
namespace Domain.Entities;

public class Comment : AuditableEntity
{
    public string AuthorName { get; set; } = default!;
    public string AuthorEmail { get; set; } = default!;
    public string Content { get; set; } = default!;

    public Guid ArticleId { get; set; }
    public Article Article { get; set; } = default!;
}