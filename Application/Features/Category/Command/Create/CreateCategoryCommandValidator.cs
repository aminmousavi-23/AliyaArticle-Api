using Application.Common.Resources;
using FluentValidation;

namespace Application.Features.Category.Command.Create;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(Messages.Category.Validation.NameRequired);
    }
}