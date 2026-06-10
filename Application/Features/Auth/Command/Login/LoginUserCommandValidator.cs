using FluentValidation;

namespace Application.Features.Auth.Command.Login;

public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        
    }
}