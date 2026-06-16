using Application.Common.Resources;
using Domain.Common.Constants.ValidationConstants;
using Domain.Enums;
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

        RuleFor(x => x.CategoryId)
            .NotEqual(Guid.Empty)
            .WithMessage(Messages.Article.Validation.CategoryRequired);

        RuleFor(x => x.Blocks)
            .NotEmpty()
            .WithMessage(Messages.Article.Validation.BlocksRequired);

        RuleForEach(x => x.Blocks)
            .SetValidator(new CreateArticleBlockValidator());
    }
}

public class CreateArticleBlockValidator : AbstractValidator<CreateArticleBlockDto>
{
    public CreateArticleBlockValidator()
    {
        RuleFor(x => x.Type)
            .IsInEnum()
            .WithMessage(Messages.Article.Validation.InvalidBlockType);

        RuleFor(x => x.Order)
            .GreaterThanOrEqualTo(0)
            .WithMessage(Messages.Article.Validation.BlockOrderInvalid);

        When(IsTextBlock, () =>
        {
            RuleFor(x => x.Text)
                .NotEmpty()
                .WithMessage(Messages.Article.Validation.BlockTextRequired);
        });

        When(IsImageBlock, () =>
        {
            RuleFor(x => x.Base64File)
                .NotNull()
                .WithMessage(Messages.Article.Validation.ImageAttachmentRequired);

            RuleFor(x => x.Base64File!.Length)
                .GreaterThan(0)
                .WithMessage(Messages.Article.Validation.ImageAttachmentRequired);
        });
    }

    private static bool IsTextBlock(CreateArticleBlockDto x)
        => x.Type is BlockType.Paragraph or BlockType.Heading or BlockType.Quote or BlockType.Code;

    private static bool IsImageBlock(CreateArticleBlockDto x)
        => x.Type == BlockType.Attachment;
}