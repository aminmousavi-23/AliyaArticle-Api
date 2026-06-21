using Application.Common.Querying.Filtering;

namespace Application.Features.Article.Queries.GetPaginated;

public class GetArticlePaginatedQuery : QueryOptions, IRequest<CollectionResponse<GetArticlePaginatedQueryResponse>>
{
}