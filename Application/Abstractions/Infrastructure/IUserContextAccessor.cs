using Application.Models.DTOs;

namespace Application.Abstractions.Infrastructure;

public interface IUserContextAccessor
{
    GetUserFromTokenDto GetUserByTokenAsync();
}