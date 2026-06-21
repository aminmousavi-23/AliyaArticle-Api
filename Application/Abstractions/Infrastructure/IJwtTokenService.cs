using Application.Models.DTOs;

namespace Application.Abstractions.Infrastructure;

public interface IJwtTokenService
{
    JwtTokenDto GenerateToken(User user);
}