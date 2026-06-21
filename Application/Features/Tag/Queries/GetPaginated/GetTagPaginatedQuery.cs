using Application.Common.Querying.Filtering;

namespace Application.Features.Tag.Queries.GetPaginated;

public class GetTagPaginatedQuery : QueryOptions, IRequest<CollectionResponse<GetTagPaginatedQueryResponse>>
{
}