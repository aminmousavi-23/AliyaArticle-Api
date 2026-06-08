using Application.Common.Querying.Filtering;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.User.Queries.GetPaginated;

public class GetUserPaginatedQuery : QueryOptions,
    IRequest<CollectionResponse<GetUserPaginatedQueryResponse>>
{
}