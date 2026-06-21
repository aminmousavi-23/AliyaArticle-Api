
namespace Application.Features.Category.Queries.GetPaginated;

public class GetCategoryPaginatedQueryHandler(
    IMapper mapper,
    ICategoryRepository categoryRepository) 
    : IRequestHandler<GetCategoryPaginatedQuery, CollectionResponse<GetCategoryPaginatedQueryResponse>>
{
    public async Task<CollectionResponse<GetCategoryPaginatedQueryResponse>> Handle(
        GetCategoryPaginatedQuery request, CancellationToken cancellationToken)
    {
        var (items, totalCount) =
            await categoryRepository.GetPaginatedAsync(request, CategoryFilterMapping.Get(), cancellationToken);
        if (items.Any() == false)
        {
            return ResponseFactory
                .CollectionNotFound<GetCategoryPaginatedQueryResponse>(Messages.Category.NotFound);
        }

        var response = mapper.Map<IEnumerable<GetCategoryPaginatedQueryResponse>>(items);

        return ResponseFactory.Ok(response, totalCount);
    }
}