using Application.Common.Querying.Filtering;
using Application.Features.Category.Queries.GetPaginated;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.Article.Queries.GetPaginated;

public class GetArticlePaginatedQuery : QueryOptions, IRequest<CollectionResponse<GetArticlePaginatedQueryResponse>>
{
}