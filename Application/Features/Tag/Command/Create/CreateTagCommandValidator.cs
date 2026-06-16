using Application.Common.Resources;
using Domain.Common.Constants.ValidationConstants;
using FluentValidation;

namespace Application.Features.Tag.Command.Create;

public class CreateTagCommandValidator : AbstractValidator<CreateTagCommand>
{
    public CreateTagCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(Messages.Tag.Validation.NameRequired)
            .MaximumLength(TagValidationConstants.NameMaxLength)
            .WithMessage(Messages.Tag.Validation.NameMaxLength);
    }
}