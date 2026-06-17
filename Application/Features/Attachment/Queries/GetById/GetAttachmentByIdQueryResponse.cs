namespace Application.Features.Attachment.Queries.GetById;

public class GetAttachmentByIdQueryResponse
{
    public byte[] Data { get; set; } = default!;
    public string ContentType { get; set; } = default!;
}