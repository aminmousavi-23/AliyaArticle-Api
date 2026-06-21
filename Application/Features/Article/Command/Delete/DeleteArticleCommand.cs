
namespace Application.Features.Article.Command.Delete;

public class DeleteArticleCommand : IRequest<BaseResponse<DeleteArticleCommandResponse>>
{
    public Guid Id { get; set; }
}