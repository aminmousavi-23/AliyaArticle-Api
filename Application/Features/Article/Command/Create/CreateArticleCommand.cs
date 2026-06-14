using Application.Features.Category.Command.Create;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.Article.Command.Create;

public class CreateArticleCommand : IRequest<BaseResponse<CreateArticleCommandResponse>>
{
    public string Title { get; set; } = default!;
    public string Summary { get; set; } = default!;
    public string Content { get; set; } = default!;

    public Guid CategoryId { get; set; }
    public List<Guid> TagIds { get; set; } = [];
}