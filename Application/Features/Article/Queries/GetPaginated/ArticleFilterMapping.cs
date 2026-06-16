using System.Linq.Expressions;
using Application.Common.Querying.Mapping;

namespace Application.Features.Article.Queries.GetPaginated;

public static class ArticleFilterMapping
{
    public static Dictionary<string, Expression<Func<Domain.Entities.Article, object>>> Get()
    {
        var baseMap = AuditableFilterMapping<Domain.Entities.Article>.Get();
        
        baseMap["title"] = x => x.Title;
        baseMap["slug"] = x => x.Slug;
        baseMap["summary"] = x => x.Summary;
        baseMap["isPublished"] = x => x.IsPublished;
        baseMap["categoryId"] = x => x.CategoryId;

        return baseMap;
    }
}