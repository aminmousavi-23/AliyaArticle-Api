namespace Domain.Entities;

public class Article : AuditableEntity
{
    public string Title { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public string Summary { get; set; } = default!;
    public bool IsPublished { get; set; }

    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = default!;

    public ICollection<ArticleBlock> Blocks { get; set; } = new List<ArticleBlock>();
    public ICollection<Tag> Tags { get; set; } = new List<Tag>();
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}