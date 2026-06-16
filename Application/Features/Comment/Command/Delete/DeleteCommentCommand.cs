using Application.Models.Responses;
using MediatR;

namespace Application.Features.Comment.Command.Delete;

public class DeleteCommentCommand : IRequest<BaseResponse<DeleteCommentCommandResponse>>
{
    public Guid Id { get; set; }
}