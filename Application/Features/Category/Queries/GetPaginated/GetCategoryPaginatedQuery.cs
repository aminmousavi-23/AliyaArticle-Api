using Application.Common.Querying.Filtering;
using Application.Features.User.Queries.GetPaginated;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.Category.Queries.GetPaginated;

public class GetCategoryPaginatedQuery : QueryOptions, IRequest<CollectionResponse<GetCategoryPaginatedQueryResponse>>
{
}