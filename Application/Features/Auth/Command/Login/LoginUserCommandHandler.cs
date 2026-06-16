using Application.Abstractions.Infrastructure;
using Application.Abstractions.Persistence;
using Application.Common.Helpers;
using Application.Common.Options;
using Application.Common.Resources;
using Application.Common.Validation;
using Application.Models.Responses;
using MediatR;
using Microsoft.Extensions.Options;

namespace Application.Features.Auth.Command.Login;

public class LoginUserCommandHandler(
    IRequestValidator requestValidator,
    IUserRepository userRepository,
    IRefreshTokenRepository refreshTokenRepository,
    IUnitOfWork unitOfWork,
    IJwtTokenService jwtTokenService,
    IOptionsMonitor<JwtConfigOptions> jwtConfigOptions)
    : IRequestHandler<LoginUserCommand, BaseResponse<LoginUserCommandResponse>>
{
    private readonly JwtConfigOptions _jwtConfigOptions = jwtConfigOptions.CurrentValue;
    
    public async Task<BaseResponse<LoginUserCommandResponse>> Handle(LoginUserCommand request,
        CancellationToken cancellationToken)
    {
        await requestValidator.ValidateAsync(request);

        var hashedRequestPassword = CredentialHelper.Hash(request.Password);

        var user = await userRepository.GetByUsernameAsync(request.Username, cancellationToken);
        if (user == null || hashedRequestPassword != user.HashedPassword)
        {
            return ResponseFactory
                .Unauthorized<LoginUserCommandResponse>(Messages.User.InvalidUsernameOrPassword);
        }

        var tokens = jwtTokenService.GenerateToken(user);

        // revoke old ones
        await refreshTokenRepository.RevokeByUserIdAsync(user.Id, cancellationToken);

        // store new refresh token
        var refreshTokenEntity = new Domain.Entities.RefreshToken
        {
            UserId = user.Id,
            TokenHash = CredentialHelper.Hash(tokens.RefreshToken),
            ExpiresAt = DateTime.UtcNow.AddDays(_jwtConfigOptions.RefreshTokenExpires)
        };

        await refreshTokenRepository.AddAsync(refreshTokenEntity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new LoginUserCommandResponse()
        {
            AccessToken = tokens.AccessToken,
            RefreshToken = tokens.RefreshToken,
            ExpiresAt = tokens.ExpiresAt,
        };

        return ResponseFactory.Ok(response);
    }
}