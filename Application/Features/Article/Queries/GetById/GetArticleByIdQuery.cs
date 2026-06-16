using Application.Models.Responses;
using MediatR;

namespace Application.Features.Article.Queries.GetById;

public class GetArticleByIdQuery : IRequest<BaseResponse<GetArticleByIdQueryResponse>>
{
    public Guid Id { get; set; }
}