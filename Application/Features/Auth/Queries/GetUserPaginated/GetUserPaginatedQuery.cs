using Application.Common.Querying.Filtering;

namespace Application.Features.Auth.Queries.GetUserPaginated;

public class GetUserPaginatedQuery : QueryOptions, IRequest<CollectionResponse<GetUserPaginatedQueryResponse>>
{
}