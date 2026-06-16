using Application.Models.Responses;
using Domain.Enums;
using MediatR;

namespace Application.Features.Article.Command.Create;

public class CreateArticleCommand : IRequest<BaseResponse<CreateArticleCommandResponse>>
{
    public string Title { get; set; } = default!;
    public string Summary { get; set; } = default!;
    
    public Guid CategoryId { get; set; }
    public List<Guid> TagIds { get; set; } = [];
    public List<CreateArticleBlockDto> Blocks { get; set; } = [];
}
public class CreateArticleBlockDto
{
    public BlockType Type { get; set; }

    public string? Text { get; set; }

    public string? Base64File { get; set; }

    public int Order { get; set; }
}