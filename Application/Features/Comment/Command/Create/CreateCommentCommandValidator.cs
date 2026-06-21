using Domain.Common.Constants.ValidationConstants;

namespace Application.Features.Comment.Command.Create;

public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
{
    public CreateCommentCommandValidator()
    {
        RuleFor(x => x.AuthorName)
            .NotEmpty()
            .WithMessage(Messages.Comment.Validation.AuthorNameRequired)
            .MaximumLength(CommentValidationConstants.AuthorNameMaxLength)
            .WithMessage(Messages.Comment.Validation.AuthorNameMaxLength);

        RuleFor(x => x.AuthorEmail)
            .NotEmpty()
            .WithMessage(Messages.Comment.Validation.AuthorEmailRequired)
            .MaximumLength(CommentValidationConstants.AuthorEmailMaxLength)
            .WithMessage(Messages.Comment.Validation.AuthorEmailMaxLength)
            .EmailAddress()
            .WithMessage(Messages.Comment.Validation.InvalidAuthorEmail);

        RuleFor(x => x.Content)
            .NotEmpty()
            .WithMessage(Messages.Comment.Validation.ContentRequired)
            .WithMessage(Messages.Comment.Validation.ContentMinLength)
            .MaximumLength(CommentValidationConstants.ContentMaxLength)
            .WithMessage(Messages.Comment.Validation.ContentMaxLength);

        RuleFor(x => x.ArticleId)
            .NotEqual(Guid.Empty)
            .WithMessage(Messages.Comment.Validation.ArticleRequired);
    }
}