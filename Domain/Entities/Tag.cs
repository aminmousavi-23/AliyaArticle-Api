using Domain.Common;

namespace Domain.Entities;

public class Tag : AuditableEntity
{
    public string Name { get; set; } = default!;
    public string Slug { get; set; } = default!;

    public ICollection<Article> Articles { get; set; } = new List<Article>();
}