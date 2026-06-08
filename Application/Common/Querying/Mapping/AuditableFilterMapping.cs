using System.Linq.Expressions;
using Domain.Common;

namespace Application.Common.Querying.Mapping;

public static class AuditableFilterMapping<TEntity>
    where TEntity : AuditableEntity
{
    public static Dictionary<string, Expression<Func<TEntity, object>>> Get()
        => new()
        {
            ["id"] = x => x.Id,
            ["createdBy"] = x => x.CreatedBy,
            ["createdAt"] = x => x.CreatedAt,
            ["lastModifiedBy"] = x => x.LastModifiedBy!,
            ["lastModifiedAt"] = x => x.LastModifiedAt!
        };
}