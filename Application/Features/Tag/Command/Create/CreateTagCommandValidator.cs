using Domain.Common.Constants.ValidationConstants;

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