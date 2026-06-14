using Application.Common.Resources;
using FluentValidation;

namespace Application.Features.Tag.Command.Create;

public class CreateTagCommandValidator : AbstractValidator<CreateTagCommand>
{
    public CreateTagCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(Messages.Tag.Validation.NameRequired);
    }
}