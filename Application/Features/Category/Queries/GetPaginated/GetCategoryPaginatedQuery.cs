using Application.Common.Querying.Filtering;

namespace Application.Features.Category.Queries.GetPaginated;

public class GetCategoryPaginatedQuery : QueryOptions, IRequest<CollectionResponse<GetCategoryPaginatedQueryResponse>>
{
}