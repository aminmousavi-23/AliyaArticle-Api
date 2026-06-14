using Application.Abstractions.Infrastructure;
using Application.Abstractions.Persistence;
using Application.Common.Helpers;
using Application.Common.Resources;
using Application.Common.Validation;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.Auth.Command.Login;

public class LoginUserCommandHandler(
    IRequestValidator requestValidator,
    IUserRepository userRepository,
    IJwtTokenService jwtTokenService)
    : IRequestHandler<LoginUserCommand, BaseResponse<LoginUserCommandResponse>>
{
    public async Task<BaseResponse<LoginUserCommandResponse>> Handle(LoginUserCommand request,
        CancellationToken cancellationToken)
    {
        await requestValidator.ValidateAsync(request);

        var hashedRequestPassword = PasswordHelper.HashPassword(request.Password);

        var user = await userRepository.GetByUsernameAsync(request.Username, cancellationToken);
        if (user == null || hashedRequestPassword != user.HashedPassword)
        {
            return ResponseFactory
                .Unauthorized<LoginUserCommandResponse>(Messages.User.InvalidUsernameOrPassword);
        }
        
        var tokens = jwtTokenService.GenerateToken(user);

        var response = new LoginUserCommandResponse()
        {
            AccessToken = tokens.AccessToken,
            RefreshToken = tokens.RefreshToken,
            ExpiresAt = tokens.ExpiresAt,
        };

        return ResponseFactory.Ok(response);
    }
}