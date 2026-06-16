using System.Linq.Expressions;
using Application.Common.Querying.Mapping;

namespace Application.Features.Auth.Queries.GetUserPaginated;

public static class UserFilterMapping
{
    public static Dictionary<string, Expression<Func<Domain.Entities.User, object>>> Get()
    {
        var baseMap = AuditableFilterMapping<Domain.Entities.User>.Get();
        
        baseMap["fullName"] = x => x.FullName;
        baseMap["phoneNumber"] = x => x.PhoneNumber;
        baseMap["email"] = x => x.Email!;
        baseMap["isAdmin"] = x => x.IsAdmin;

        return baseMap;
    }
}