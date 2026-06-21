namespace Application.Features.Article.Command.Delete;

public class DeleteArticleCommandHandler(
    IRequestValidator requestValidator,
    IArticleRepository articleRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteArticleCommand, BaseResponse<DeleteArticleCommandResponse>>
{
    public async Task<BaseResponse<DeleteArticleCommandResponse>> Handle(DeleteArticleCommand request,
        CancellationToken cancellationToken)
    {
        await requestValidator.ValidateAsync(request);

        var article = await articleRepository.GetByIdAsync(request.Id, cancellationToken);
        if (article == null)
        {
            return ResponseFactory.NotFound<DeleteArticleCommandResponse>(Messages.Article.NotFound);
        }

        articleRepository.Remove(article);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ResponseFactory.Ok<DeleteArticleCommandResponse>(Messages.Article.Deleted);
    }
}