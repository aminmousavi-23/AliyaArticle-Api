using Application.Abstractions.Persistence;
using Application.Common.Querying.Mapping;
using Application.Common.Resources;
using Application.Models.Responses;
using AutoMapper;
using MediatR;

namespace Application.Features.User.Queries.GetPaginated;

public class GetUserPaginatedQueryHandler(
    IMapper mapper,
    IUserRepository userRepository) 
    : IRequestHandler<GetUserPaginatedQuery, CollectionResponse<GetUserPaginatedQueryResponse>>
{
    public async Task<CollectionResponse<GetUserPaginatedQueryResponse>> Handle(
        GetUserPaginatedQuery request, CancellationToken cancellationToken)
    {
        var (items, totalCount) =
            await userRepository.GetPaginatedAsync(request, UserFilterMapping.Get(), cancellationToken);
        if (items.Any() == false)
        {
            return ResponseFactory
                .CollectionNotFound<GetUserPaginatedQueryResponse>(Messages.User.NotFound);
        }

        var response = mapper.Map<IEnumerable<GetUserPaginatedQueryResponse>>(items);

        return ResponseFactory.Ok(response, totalCount);
    }
}