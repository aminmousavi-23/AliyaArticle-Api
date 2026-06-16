using Application.Models.Responses;
using MediatR;

namespace Application.Features.Comment.Command.Create;

public class CreateCommentCommand : IRequest<BaseResponse<CreateCommentCommandResponse>>
{
    public string AuthorName { get; set; } = default!;
    public string AuthorEmail { get; set; } = default!;
    public string Content { get; set; } = default!;

    public Guid ArticleId { get; set; }
}