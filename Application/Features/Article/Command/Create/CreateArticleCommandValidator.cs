using Application.Common.Resources;
using Domain.Common.Constants.ValidationConstants;
using FluentValidation;

namespace Application.Features.Article.Command.Create;

public class CreateArticleCommandValidator : AbstractValidator<CreateArticleCommand>
{
    public CreateArticleCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage(Messages.Article.Validation.TitleRequired)
            .MaximumLength(ArticleValidationConstants.TitleMaxLength)
            .WithMessage(Messages.Article.Validation.TitleMaxLength);

        RuleFor(x => x.Summary)
            .NotEmpty()
            .WithMessage(Messages.Article.Validation.SummaryRequired)
            .MaximumLength(ArticleValidationConstants.SummaryMaxLength)
            .WithMessage(Messages.Article.Validation.SummaryMaxLength);

        RuleFor(x => x.Content)
            .NotEmpty()
            .WithMessage(Messages.Article.Validation.ContentRequired);

        RuleFor(x => x.CategoryId)
            .NotEqual(Guid.Empty)
            .WithMessage(Messages.Article.Validation.CategoryRequired);
    }
}