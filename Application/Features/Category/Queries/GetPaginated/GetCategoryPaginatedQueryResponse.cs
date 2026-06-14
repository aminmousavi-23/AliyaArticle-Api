using Application.Models.ViewModels;

namespace Application.Features.Category.Queries.GetPaginated;

public class GetCategoryPaginatedQueryResponse : AuditableViewModel
{
    public string Name { get; set; } = default!;
    public string Slug { get; set; } = default!;
}