using Application.Common.Resources;
using FluentValidation;

namespace Application.Features.User.Command.Register;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage(Messages.User.Validation.FullNameRequired);

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage(Messages.User.Validation.PhoneNumberRequired)
            .Matches(@"^09\d{9}$").WithMessage(Messages.User.Validation.InvalidPhoneNumber);

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage(Messages.User.Validation.PasswordRequired);

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password).WithMessage(Messages.User.Validation.PasswordsAreNotEqual);
        
        RuleFor(x => x.Email)
            .EmailAddress().When(x => string.IsNullOrEmpty(x.Email) != true)
            .WithMessage(Messages.User.Validation.InvalidEmail);
    }
}