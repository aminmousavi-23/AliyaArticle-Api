using System.Linq.Expressions;
using Application.Common.Querying.Mapping;

namespace Application.Features.Tag.Queries.GetPaginated;

public static class TagFilterMapping
{
    public static Dictionary<string, Expression<Func<Domain.Entities.Tag, object>>> Get()
    {
        var baseMap = AuditableFilterMapping<Domain.Entities.Tag>.Get();
        
        baseMap["name"] = x => x.Name;
        baseMap["slug"] = x => x.Slug;

        return baseMap;
    }
}