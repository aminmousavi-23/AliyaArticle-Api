using Application.Abstractions.Persistence;
using Application.Common.Resources;
using Application.Models.Responses;
using AutoMapper;
using MediatR;

namespace Application.Features.Article.Queries.GetById;

public class GetArticleByIdQueryHandler(
    IMapper mapper,
    IArticleRepository articleRepository) 
    : IRequestHandler<GetArticleByIdQuery, BaseResponse<GetArticleByIdQueryResponse>>
{
    public async Task<BaseResponse<GetArticleByIdQueryResponse>> Handle(
        GetArticleByIdQuery request, CancellationToken cancellationToken)
    {
        var article = await articleRepository.GetByIdWithDetail(request.Id, cancellationToken);
        if (article == null)
        {
            return ResponseFactory
                .NotFound<GetArticleByIdQueryResponse>(Messages.Article.NotFound);
        }

        var response = mapper.Map<GetArticleByIdQueryResponse>(article);

        return ResponseFactory.Ok(response);
    }
}