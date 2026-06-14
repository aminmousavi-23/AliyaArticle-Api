using Domain.Common;

namespace Domain.Entities;

public class Article : AuditableEntity
{
    public string Title { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public string Summary { get; set; } = default!;
    public string Content { get; set; } = default!;
    //TODO:Images
    public bool IsPublished { get; set; }

    public Guid AuthorId { get; set; }
    public User Author { get; set; } = default!;

    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = default!;

    public ICollection<Tag> Tags { get; set; } = new List<Tag>();
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}