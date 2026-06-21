
namespace Application.Features.Tag.Queries.GetPaginated;

public class GetTagPaginatedQueryHandler(
    IMapper mapper,
    ITagRepository tagRepository) 
    : IRequestHandler<GetTagPaginatedQuery, CollectionResponse<GetTagPaginatedQueryResponse>>
{
    public async Task<CollectionResponse<GetTagPaginatedQueryResponse>> Handle(
        GetTagPaginatedQuery request, CancellationToken cancellationToken)
    {
        var (items, totalCount) =
            await tagRepository.GetPaginatedAsync(request, TagFilterMapping.Get(), cancellationToken);
        if (items.Any() == false)
        {
            return ResponseFactory
                .CollectionNotFound<GetTagPaginatedQueryResponse>(Messages.Tag.NotFound);
        }

        var response = mapper.Map<IEnumerable<GetTagPaginatedQueryResponse>>(items);

        return ResponseFactory.Ok(response, totalCount);
    }
}