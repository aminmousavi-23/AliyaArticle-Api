using Application.Models.Responses;
using MediatR;

namespace Application.Features.Article.Command.Publish;

public class PublishArticleCommand : IRequest<BaseResponse<PublishArticleCommandResponse>>
{
    public Guid Id { get; set; }
}