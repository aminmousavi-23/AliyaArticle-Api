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
            ["createdDate"] = x => x.CreatedDate,
            ["lastModifiedBy"] = x => x.LastModifiedBy!,
            ["lastModifiedDate"] = x => x.LastModifiedDate!
        };
}