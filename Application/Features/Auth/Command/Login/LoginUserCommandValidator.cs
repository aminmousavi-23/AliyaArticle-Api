using Domain.Common.Constants.ValidationConstants;

namespace Application.Features.Auth.Command.Login;

public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage(Messages.User.Validation.UsernameRequired)
            .MaximumLength(UserValidationConstants.UsernameMaxLength)
            .WithMessage(Messages.User.Validation.UsernameMaxLength);

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage(Messages.User.Validation.PasswordRequired)
            .MinimumLength(UserValidationConstants.PasswordMinLength)
            .WithMessage(Messages.User.Validation.PasswordMinLength)
            .MaximumLength(UserValidationConstants.PasswordMaxLength)
            .WithMessage(Messages.User.Validation.PasswordMaxLength);
    }
}