using Application.Common.Querying.Filtering;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.Auth.Queries.GetUserPaginated;

public class GetUserPaginatedQuery : QueryOptions, IRequest<CollectionResponse<GetUserPaginatedQueryResponse>>
{
}