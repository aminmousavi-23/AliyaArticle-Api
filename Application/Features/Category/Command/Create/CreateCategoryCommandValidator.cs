using Application.Common.Resources;
using Domain.Common.Constants.ValidationConstants;
using FluentValidation;

namespace Application.Features.Category.Command.Create;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(Messages.Category.Validation.NameRequired)
            .MaximumLength(CategoryValidationConstants.NameMaxLength)
            .WithMessage(Messages.Category.Validation.NameMaxLength);
    }
}