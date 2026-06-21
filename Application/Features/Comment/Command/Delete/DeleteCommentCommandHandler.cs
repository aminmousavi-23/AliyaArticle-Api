
namespace Application.Features.Comment.Command.Delete;

public class DeleteCommentCommandHandler(
    IRequestValidator requestValidator,
    ICommentRepository commentRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteCommentCommand, BaseResponse<DeleteCommentCommandResponse>>
{
    public async Task<BaseResponse<DeleteCommentCommandResponse>> Handle(DeleteCommentCommand request,
        CancellationToken cancellationToken)
    {
        await requestValidator.ValidateAsync(request);

        var comment = await commentRepository.GetByIdAsync(request.Id, cancellationToken);
        if (comment == null)
        {
            return ResponseFactory.NotFound<DeleteCommentCommandResponse>(Messages.Comment.NotFound);
        }

        commentRepository.Remove(comment);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ResponseFactory.Ok<DeleteCommentCommandResponse>(Messages.Comment.Deleted);
    }
}