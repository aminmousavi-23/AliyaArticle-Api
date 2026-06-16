using Application.Features.Category.Queries.GetPaginated;
using Application.Features.Tag.Queries.GetPaginated;
using Application.Models.ViewModels;
using Domain.Enums;

namespace Application.Features.Article.Queries.GetById;

public class GetArticleByIdQueryResponse : AuditableViewModel
{
    public string Title { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public string Summary { get; set; } = default!;
    public bool IsPublished { get; set; }
    
    public GetCategoryPaginatedQueryResponse Category { get; set; } = default!;

    public List<GetTagPaginatedQueryResponse> Tags { get; set; } = [];
    public List<GetArticleBlockDto> Blocks { get; set; } = [];
}
public class GetArticleBlockDto
{
    public int Order { get; set; }
    public BlockType Type { get; set; }

    public string? Text { get; set; }

    public Guid? AttachmentId { get; set; }
}