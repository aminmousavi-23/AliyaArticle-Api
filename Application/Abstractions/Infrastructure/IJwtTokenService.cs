using Application.Models.DTOs;
using Domain.Entities;

namespace Application.Abstractions.Infrastructure;

public interface IJwtTokenService
{
    JwtTokenDto GenerateToken(User user);
}