using Domain.Common;

namespace Domain.Entities;

public class Attachment : AuditableEntity
{
    public byte[] Data { get; set; } = default!;
    public string ContentType { get; set; } = default!;
    public long Size { get; set; }
}