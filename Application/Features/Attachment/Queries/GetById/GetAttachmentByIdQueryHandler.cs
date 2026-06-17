using Application.Abstractions.Persistence;
using Application.Common.Resources;
using Application.Exceptions;
using Application.Models.Responses;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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