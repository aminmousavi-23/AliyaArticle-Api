using Application.Common.Resources;
using FluentValidation;

namespace Application.Features.Article.Command.Create;

public class CreateArticleCommandValidator : AbstractValidator<CreateArticleCommand>
{
    public CreateArticleCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage(Messages.Category.Validation.NameRequired);
    }
}