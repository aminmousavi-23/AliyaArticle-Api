using Application.Common.Helpers;
using Domain.Common.Constants.ValidationConstants;
using Domain.Enums;

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
    private const int MaxFileSize = 3 * 1024 * 1024;

    private static readonly string[] AllowedTypes =
    [
        "image/png",
        "image/jpeg",
        "application/pdf"
    ];

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

        When(IsFileBlock, () =>
        {
            RuleFor(x => x.Base64File)
                .NotEmpty()
                .WithMessage(Messages.Article.Validation.Base64FileRequired)
                .Must(BeValidBase64)
                .WithMessage(Messages.Article.Validation.FileTypeNotAllowed)
                .Must(BeValidFileSize)
                .WithMessage(Messages.Article.Validation.FileSizeExceeded)
                .Must(BeValidFileType)
                .WithMessage(Messages.Article.Validation.FileTypeNotAllowed);
        });
    }

    #region Private Methods

    private static bool IsTextBlock(CreateArticleBlockDto x)
        => x.Type is BlockType.Paragraph or BlockType.Heading or BlockType.Quote or BlockType.Code;

    private static bool IsFileBlock(CreateArticleBlockDto x)
        => x.Type == BlockType.Attachment;

    private static bool BeValidBase64(string? base64)
    {
        if (string.IsNullOrWhiteSpace(base64))
            return false;

        Span<byte> buffer = new byte[base64.Length];

        return Convert.TryFromBase64String(
            base64,
            buffer,
            out _
        );
    }

    private static bool BeValidFileSize(string? base64)
    {
        if (!TryGetBytes(base64, out var bytes))
            return false;

        return bytes.Length <= MaxFileSize;
    }

    private static bool BeValidFileType(string? base64)
    {
        if (!TryGetBytes(base64, out var bytes))
            return false;

        return AttachmentHelper.IsAllowedType(bytes, AllowedTypes);
    }

    private static bool TryGetBytes(string? base64, out byte[] bytes)
    {
        bytes = [];

        if (string.IsNullOrWhiteSpace(base64))
            return false;

        try
        {
            bytes = Convert.FromBase64String(base64);
            return true;
        }
        catch
        {
            return false;
        }
    }

    #endregion
}