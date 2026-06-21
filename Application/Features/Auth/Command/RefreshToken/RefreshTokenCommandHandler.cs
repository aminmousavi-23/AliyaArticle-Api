using Application.Abstractions.Infrastructure;
using Application.Abstractions.Persistence;
using Application.Common.Helpers;
using Application.Common.Options;
using Application.Common.Resources;
using Application.Models.Responses;
using MediatR;
using Microsoft.Extensions.Options;

namespace Application.Features.Auth.Command.RefreshToken;

public class RefreshTokenCommandHandler(
    IRefreshTokenRepository refreshTokenRepository,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IJwtTokenService jwtTokenService,
    IOptionsMonitor<JwtConfigOptions> jwtConfigOptions)
    : IRequestHandler<RefreshTokenCommand, BaseResponse<RefreshTokenCommandResponse>>
{
    private readonly JwtConfigOptions _jwtConfigOptions = jwtConfigOptions.CurrentValue;
    
    public async Task<BaseResponse<RefreshTokenCommandResponse>> Handle(RefreshTokenCommand request,
        CancellationToken cancellationToken)
    {
        var refreshTokenHash = CredentialHelper.HashSha256(request.RefreshToken);

        var storedToken = await refreshTokenRepository.GetByHashAsync(refreshTokenHash, cancellationToken);
        if (storedToken == null || storedToken.IsRevoked || storedToken.ExpiresAt < DateTime.UtcNow)
        {
            return ResponseFactory
                .Unauthorized<RefreshTokenCommandResponse>(Messages.Auth.InvalidToken);
        }
        
        var user = await userRepository.GetByIdAsync(storedToken.UserId, cancellationToken);
        if (user == null || user.IsActive == false)
        {
            return ResponseFactory
                .Unauthorized<RefreshTokenCommandResponse>(Messages.User.IsNotActive);
        }
        
        // generate new tokens
        var tokens = jwtTokenService.GenerateToken(user);

        // revoke old refresh token
        storedToken.IsRevoked = true;
        
        var newRefreshToken = new Domain.Entities.RefreshToken
        {
            UserId = user.Id,
            TokenHash = CredentialHelper.HashSha256(tokens.RefreshToken),
            ExpiresAt = DateTime.UtcNow.AddDays(_jwtConfigOptions.RefreshTokenExpires)
        };

        await refreshTokenRepository.AddAsync(newRefreshToken, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new RefreshTokenCommandResponse
        {
            AccessToken = tokens.AccessToken,
            RefreshToken = tokens.RefreshToken,
            ExpiresAt = tokens.ExpiresAt
        };

        return ResponseFactory.Ok(response);
    }
}