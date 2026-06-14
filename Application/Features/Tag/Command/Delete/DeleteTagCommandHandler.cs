using Application.Abstractions.Persistence;
using Application.Common.Resources;
using Application.Common.Validation;
using Application.Features.Category.Command.Delete;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.Tag.Command.Delete;

public class DeleteTagCommandHandler(
    IRequestValidator requestValidator,
    ITagRepository tagRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteTagCommand, BaseResponse<DeleteTagCommandResponse>>
{
    public async Task<BaseResponse<DeleteTagCommandResponse>> Handle(DeleteTagCommand request,
        CancellationToken cancellationToken)
    {
        await requestValidator.ValidateAsync(request);

        var tag = await tagRepository.GetByIdAsync(request.Id, cancellationToken);
        if (tag == null)
        {
            return ResponseFactory.NotFound<DeleteTagCommandResponse>(Messages.Tag.NotFound);
        }

        var hasArticles = await tagRepository.HasArticlesAsync(request.Id, cancellationToken);
        if (hasArticles)
        {
            return ResponseFactory.Conflict<DeleteTagCommandResponse>(Messages.Tag.CanNotDelete);
        }

        tagRepository.Remove(tag);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ResponseFactory.Ok<DeleteTagCommandResponse>(Messages.Tag.Deleted);
    }
}