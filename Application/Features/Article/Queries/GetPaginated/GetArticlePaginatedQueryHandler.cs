namespace Application.Features.Article.Queries.GetPaginated;

public class GetArticlePaginatedQueryHandler(
    IMapper mapper,
    IArticleRepository articleRepository) 
    : IRequestHandler<GetArticlePaginatedQuery, CollectionResponse<GetArticlePaginatedQueryResponse>>
{
    public async Task<CollectionResponse<GetArticlePaginatedQueryResponse>> Handle(
        GetArticlePaginatedQuery request, CancellationToken cancellationToken)
    {
        var (items, totalCount) =
            await articleRepository.GetPaginatedAsync(request, ArticleFilterMapping.Get(), cancellationToken);
        if (items.Any() == false)
        {
            return ResponseFactory
                .CollectionNotFound<GetArticlePaginatedQueryResponse>(Messages.Article.NotFound);
        }

        var response = mapper.Map<IEnumerable<GetArticlePaginatedQueryResponse>>(items);

        return ResponseFactory.Ok(response, totalCount);
    }
}