using Application.Features.Category.Queries.GetPaginated;
using Application.Models.ViewModels;

namespace Application.Features.Article.Queries.GetPaginated;

public class GetArticlePaginatedQueryResponse : AuditableViewModel
{
    public string Title { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public string Summary { get; set; } = default!;
    public bool IsPublished { get; set; }
    
    public GetCategoryPaginatedQueryResponse Category { get; set; } = default!;
}