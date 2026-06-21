using Application.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Attachment.Queries.GetById;

public class GetAttachmentByIdQueryHandler(
    IMapper mapper,
    IAttachmentRepository attachmentRepository) 
    : IRequestHandler<GetAttachmentByIdQuery, BaseResponse<GetAttachmentByIdQueryResponse>>
{
    public async Task<BaseResponse<GetAttachmentByIdQueryResponse>> Handle(
        GetAttachmentByIdQuery request, CancellationToken cancellationToken)
    {
        var attachment = await attachmentRepository.GetByIdAsync(request.Id, cancellationToken);
        if (attachment == null)
        {
            throw new AppException(Messages.Attachment.NotFound, StatusCodes.Status203NonAuthoritative);
        }
        
        var response = mapper.Map<GetAttachmentByIdQueryResponse>(attachment);

        return ResponseFactory.Ok(response);
    }
}