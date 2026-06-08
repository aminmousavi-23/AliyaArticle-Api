using System.Linq.Expressions;
using Domain.Entities;

namespace Application.Common.Querying.Mapping;

public static class UserFilterMapping
{
    public static Dictionary<string, Expression<Func<User, object>>> Get()
    {
        var baseMap = AuditableFilterMapping<User>.Get();
        
        baseMap["fullName"] = x => x.FullName;
        baseMap["phoneNumber"] = x => x.PhoneNumber;
        baseMap["email"] = x => x.Email!;
        baseMap["isAdmin"] = x => x.IsAdmin;

        return baseMap;
    }
}