using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.Abstractions.Infrastructure;
using Application.Common.Options;
using Application.Common.Resources;
using Application.Exceptions;
using Application.Models.DTOs;
using Infrastructure.Common.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services;

public class JwtTokenService(IOptionsMonitor<JwtConfigOptions> jwtConfigOptions) : IJwtTokenService
{
    private readonly JwtConfigOptions _jwtConfigOptions = jwtConfigOptions.CurrentValue;
    
    public JwtTokenDto GenerateToken(User user)
    {
        if (user.IsActive == false)
            throw new AppException(Messages.User.IsNotActive, StatusCodes.Status401Unauthorized);
        
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfigOptions.Key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new List<Claim>
        {
            new Claim(CustomClaimTypes.UserId, user.Id.ToString()),
            new Claim(CustomClaimTypes.Username, user.Username),
            new Claim(CustomClaimTypes.FullName, user.FullName),
            new Claim(CustomClaimTypes.PhoneNumber, user.PhoneNumber),
            new(CustomClaimTypes.IsActive, user.IsActive.ToString().ToLowerInvariant())
        };
        if (user.IsAdmin)
        {
            claims.Add(new Claim(ClaimTypes.Role, "Admin"));
        }
        
        var expiresAt = DateTime.UtcNow.AddMinutes(_jwtConfigOptions.AccessTokenExpires);
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = expiresAt,
            SigningCredentials = credentials,
            Issuer = _jwtConfigOptions.Issuer,
            Audience = _jwtConfigOptions.Audience
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        var response = new JwtTokenDto()
        {
            AccessToken = tokenHandler.WriteToken(token),
            RefreshToken = GenerateRefreshToken(),
            ExpiresAt = expiresAt
        };

        return response;
    }
    
    private static string GenerateRefreshToken()
    {
        var randomBytes = RandomNumberGenerator.GetBytes(64);
        return Convert.ToBase64String(randomBytes);
    }
}