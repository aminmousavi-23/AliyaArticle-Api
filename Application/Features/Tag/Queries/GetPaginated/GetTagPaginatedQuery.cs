using Application.Common.Querying.Filtering;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.Tag.Queries.GetPaginated;

public class GetTagPaginatedQuery : QueryOptions, IRequest<CollectionResponse<GetTagPaginatedQueryResponse>>
{
}