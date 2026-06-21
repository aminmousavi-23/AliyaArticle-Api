using Application.Common.Querying.Filtering;

namespace Application.Features.Comment.Queries.GetPaginated;

public class GetCommentPaginatedQuery : QueryOptions, IRequest<CollectionResponse<GetCommentPaginatedQueryResponse>>
{
    public Guid ArticleId { get; set; }
}