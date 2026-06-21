
namespace Application.Features.Comment.Queries.GetPaginated;

public class GetCommentPaginatedQueryHandler(
    IMapper mapper,
    ICommentRepository commentRepository) 
    : IRequestHandler<GetCommentPaginatedQuery, CollectionResponse<GetCommentPaginatedQueryResponse>>
{
    public async Task<CollectionResponse<GetCommentPaginatedQueryResponse>> Handle(
        GetCommentPaginatedQuery request, CancellationToken cancellationToken)
    {
        var (items, totalCount) =
            await commentRepository.GetPaginatedAsync(request, CommentFilterMapping.Get(), cancellationToken);
        if (items.Any() == false)
        {
            return ResponseFactory
                .CollectionNotFound<GetCommentPaginatedQueryResponse>(Messages.Comment.NotFound);
        }

        var response = mapper.Map<IEnumerable<GetCommentPaginatedQueryResponse>>(items);

        return ResponseFactory.Ok(response, totalCount);
    }
}