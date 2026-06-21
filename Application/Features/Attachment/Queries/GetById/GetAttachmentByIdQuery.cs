
namespace Application.Features.Attachment.Queries.GetById;

public class GetAttachmentByIdQuery : IRequest<BaseResponse<GetAttachmentByIdQueryResponse>>
{
    public Guid Id { get; set; }
}