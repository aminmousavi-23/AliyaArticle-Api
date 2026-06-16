using Application.Models.ViewModels;

namespace Application.Features.Comment.Queries.GetPaginated;

public class GetCommentPaginatedQueryResponse : AuditableViewModel
{
    public string AuthorName { get; set; } = default!;
    public string AuthorEmail { get; set; } = default!;
    public string Content { get; set; } = default!;

    public Guid ArticleId { get; set; }
}