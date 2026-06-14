using Application.Models.ViewModels;

namespace Application.Features.Tag.Queries.GetPaginated;

public class GetTagPaginatedQueryResponse : AuditableViewModel
{
    public string Name { get; set; } = default!;
    public string Slug { get; set; } = default!;
}