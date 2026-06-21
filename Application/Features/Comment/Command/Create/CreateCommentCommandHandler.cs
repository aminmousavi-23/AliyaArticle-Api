
namespace Application.Features.Comment.Command.Create;

public class CreateCommentCommandHandler(
    IMapper mapper,
    IRequestValidator requestValidator,
    ICommentRepository commentRepository,
    IArticleRepository articleRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateCommentCommand, BaseResponse<CreateCommentCommandResponse>>
{
    public async Task<BaseResponse<CreateCommentCommandResponse>> Handle(CreateCommentCommand request,
        CancellationToken cancellationToken)
    {
        await requestValidator.ValidateAsync(request);
        
        var article = await articleRepository.GetByIdAsync(request.ArticleId, cancellationToken);
        if (article == null || article.IsPublished == false)
        {
            return ResponseFactory.NotFound<CreateCommentCommandResponse>(Messages.Article.NotFound);
        }

        var newComment = mapper.Map<Domain.Entities.Comment>(request);

        await commentRepository.AddAsync(newComment, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var response = mapper.Map<CreateCommentCommandResponse>(newComment);
        
        return ResponseFactory.Created(response, Messages.Comment.Created);
    }
}