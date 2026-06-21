using Application.Abstractions.Persistence;
using Application.Common.Resources;
using Application.Common.Validation;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.Article.Command.Publish;

public class PublishArticleCommandHandler(
    IRequestValidator requestValidator,
    IArticleRepository articleRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<PublishArticleCommand, BaseResponse<PublishArticleCommandResponse>>
{
    public async Task<BaseResponse<PublishArticleCommandResponse>> Handle(PublishArticleCommand request,
        CancellationToken cancellationToken)
    {
        await requestValidator.ValidateAsync(request);

        var article = await articleRepository.GetByIdAsync(request.Id, cancellationToken);
        if (article == null)
        {
            return ResponseFactory.NotFound<PublishArticleCommandResponse>(Messages.Article.NotFound);
        }
        
        article.IsPublished = true;
        
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ResponseFactory.Ok<PublishArticleCommandResponse>(Messages.Article.Published);
    }
}