using System.Linq.Expressions;
using Application.Common.Querying.Mapping;

namespace Application.Features.Category.Queries.GetPaginated;

public static class CategoryFilterMapping
{
    public static Dictionary<string, Expression<Func<Domain.Entities.Category, object>>> Get()
    {
        var baseMap = AuditableFilterMapping<Domain.Entities.Category>.Get();
        
        baseMap["name"] = x => x.Name;
        baseMap["slug"] = x => x.Slug;

        return baseMap;
    }
}