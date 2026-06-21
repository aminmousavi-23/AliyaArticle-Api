
namespace Application.Features.Category.Command.Delete;

public class DeleteCategoryCommandHandler(
    IRequestValidator requestValidator,
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteCategoryCommand, BaseResponse<DeleteCategoryCommandResponse>>
{
    public async Task<BaseResponse<DeleteCategoryCommandResponse>> Handle(DeleteCategoryCommand request,
        CancellationToken cancellationToken)
    {
        await requestValidator.ValidateAsync(request);

        var category = await categoryRepository.GetByIdAsync(request.Id, cancellationToken);
        if (category == null)
        {
            return ResponseFactory.NotFound<DeleteCategoryCommandResponse>(Messages.Category.NotFound);
        }

        var hasArticles = await categoryRepository.HasArticlesAsync(request.Id, cancellationToken);
        if (hasArticles)
        {
            return ResponseFactory.Conflict<DeleteCategoryCommandResponse>(Messages.Category.CanNotDelete);
        }

        categoryRepository.Remove(category);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ResponseFactory.Ok<DeleteCategoryCommandResponse>(Messages.Category.Deleted);
    }
}