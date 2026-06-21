using Domain.Common.Constants.ValidationConstants;

namespace Application.Features.Auth.Command.Register;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage(Messages.User.Validation.UsernameRequired)
            .MaximumLength(UserValidationConstants.UsernameMaxLength)
            .WithMessage(Messages.User.Validation.UsernameMaxLength);

        RuleFor(x => x.FullName)
            .NotEmpty()
            .WithMessage(Messages.User.Validation.FullNameRequired)
            .MaximumLength(UserValidationConstants.FullNameMaxLength)
            .WithMessage(Messages.User.Validation.FullNameMaxLength);

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage(Messages.User.Validation.PhoneNumberRequired)
            .MaximumLength(UserValidationConstants.PhoneNumberMaxLength)
            .WithMessage(Messages.User.Validation.PhoneNumberMaxLength)
            .Matches(@"^09\d{9}$")
            .WithMessage(Messages.User.Validation.InvalidPhoneNumber);

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage(Messages.User.Validation.PasswordRequired)
            .MinimumLength(UserValidationConstants.PasswordMinLength)
            .WithMessage(Messages.User.Validation.PasswordMinLength)
            .MaximumLength(UserValidationConstants.PasswordMaxLength)
            .WithMessage(Messages.User.Validation.PasswordMaxLength);

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password)
            .WithMessage(Messages.User.Validation.PasswordsAreNotEqual);

        RuleFor(x => x.Email)
            .MaximumLength(UserValidationConstants.EmailMaxLength)
            .WithMessage(Messages.User.Validation.EmailMaxLength)
            .EmailAddress()
            .When(x => !string.IsNullOrWhiteSpace(x.Email))
            .WithMessage(Messages.User.Validation.InvalidEmail);
    }
}