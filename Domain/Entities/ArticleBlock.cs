using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class ArticleBlock : AuditableEntity
{
    public Guid ArticleId { get; set; }
    public Article Article { get; set; } = default!;

    public int Order { get; set; }

    public BlockType Type { get; set; }

    public string? Text { get; set; }

    public Guid? AttachmentId { get; set; }
    public Attachment? Attachment { get; set; }
}