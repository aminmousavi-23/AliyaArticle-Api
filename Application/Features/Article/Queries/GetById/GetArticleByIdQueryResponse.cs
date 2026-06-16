using Application.Features.Comment.Queries.GetPaginated;
using Application.Features.Tag.Queries.GetPaginated;
using Application.Models.ViewModels;

namespace Application.Features.Article.Queries.GetById;

public class GetArticleByIdQueryResponse : AuditableViewModel
{
    public string Title { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public string Summary { get; set; } = default!;
    public string Content { get; set; } = default!;
    public bool IsPublished { get; set; }

    public Guid CategoryId { get; set; }

    public List<GetTagPaginatedQueryResponse> Tags { get; set; } = [];
}