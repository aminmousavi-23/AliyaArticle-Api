using System.Linq.Expressions;
using Application.Common.Querying.Mapping;

namespace Application.Features.Comment.Queries.GetPaginated;

public static class CommentFilterMapping
{
    public static Dictionary<string, Expression<Func<Domain.Entities.Comment, object>>> Get()
    {
        var baseMap = AuditableFilterMapping<Domain.Entities.Comment>.Get();
        
        baseMap["authorName"] = x => x.AuthorName;
        baseMap["authorEmail"] = x => x.AuthorEmail;
        baseMap["content"] = x => x.Content;
        baseMap["articleId"] = x => x.ArticleId;

        return baseMap;
    }
}