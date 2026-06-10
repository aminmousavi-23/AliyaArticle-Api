using System.IdentityModel.Tokens.Jwt;
using Application.Abstractions.Infrastructure;
using Application.Common.Resources;
using Application.Features.User.Queries.GetById;
using Infrastructure.Common.Constants;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services;

public class UserContextAccessor(
    IHttpContextAccessor httpContextAccessor)
    : IUserContextAccessor
{
    private const string SystemUsername = "0";
    private const string SystemFullName = "سیستم";

    public GetUserByIdQueryResponse GetUserByTokenAsync()
    {
        var user = httpContextAccessor.HttpContext?.User;

        if (user?.Identity?.IsAuthenticated != true)
            return new GetUserByIdQueryResponse()
            {
                Username = SystemUsername,
                FullName = SystemFullName
            };

        var username = user.FindFirst(CustomClaimTypes.Username)?.Value
                       ?? throw new AuthenticationFailureException(Messages.Auth.InvalidToken);

        var fullName = user.FindFirst(CustomClaimTypes.FullName)?.Value
                       ?? throw new AuthenticationFailureException(Messages.Auth.InvalidToken);
        
        var isAdmin = bool.Parse(user.FindFirst(CustomClaimTypes.IsAdmin)?.Value
                                 ?? throw new AuthenticationFailureException(Messages.Auth.InvalidToken));

        var isActive = bool.Parse(user.FindFirst(CustomClaimTypes.IsActive)?.Value
                                  ?? throw new AuthenticationFailureException(Messages.Auth.InvalidToken));

        return new GetUserByIdQueryResponse()
        {
            Username = username,
            FullName = fullName,
            IsAdmin = isAdmin,
            IsActive = isActive
        };
    }
}