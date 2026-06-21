using Application.Common.Helpers;

namespace Application.Features.Article.Command.Create;

public class CreateArticleCommandHandler(
    IMapper mapper,
    IRequestValidator requestValidator,
    IArticleRepository articleRepository,
    ICategoryRepository categoryRepository,
    ITagRepository tagRepository,
    IAttachmentRepository attachmentRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateArticleCommand, BaseResponse<CreateArticleCommandResponse>>
{
    public async Task<BaseResponse<CreateArticleCommandResponse>> Handle(CreateArticleCommand request,
        CancellationToken cancellationToken)
    {
        await requestValidator.ValidateAsync(request);

        var category = await categoryRepository.GetByIdAsync(request.CategoryId, cancellationToken);
        if (category == null)
        {
            return ResponseFactory.NotFound<CreateArticleCommandResponse>(Messages.Category.NotFound);
        }

        var slug = SlugHelper.GenerateSlug(request.Title);

        var articleExists = await articleRepository.ExistsAsync(slug, cancellationToken);
        if (articleExists)
            slug = $"{slug}-{Guid.NewGuid().ToString()[..6]}";

        var tags = new List<Domain.Entities.Tag>();
        if (request.TagIds.Any())
        {
            tags = await tagRepository.GetByIdsAsync(request.TagIds, cancellationToken);
        }

        var blocks = await BuildBlocksAsync(request.Blocks, cancellationToken);

        var newArticle = mapper.Map<Domain.Entities.Article>(request);
        newArticle.Slug = slug;
        newArticle.Tags = tags;
        newArticle.Blocks = blocks;

        await articleRepository.AddAsync(newArticle, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var response = mapper.Map<CreateArticleCommandResponse>(newArticle);

        return ResponseFactory.Created(response, Messages.Article.Created);
    }

    #region Private Methods

    private async Task<List<ArticleBlock>> BuildBlocksAsync(List<CreateArticleBlockDto> dtos, 
        CancellationToken cancellationToken)
    {
        var blocks = new List<ArticleBlock>();

        foreach (var dto in dtos.OrderBy(x => x.Order))
        {
            var attachmentId = await TryCreateAttachmentAsync(dto, cancellationToken);

            blocks.Add(new ArticleBlock
            {
                Type = dto.Type,
                Text = dto.Text,
                AttachmentId = attachmentId,
                Order = dto.Order
            });
        }

        return blocks;
    }
    
    private async Task<Guid?> TryCreateAttachmentAsync(CreateArticleBlockDto dto, CancellationToken cancellationToken)
    {
        var base64 = dto.Base64File;
        
        if (string.IsNullOrWhiteSpace(base64))
            return null;

        var bytes = Convert.FromBase64String(base64);

        var attachment = new Domain.Entities.Attachment
        {
            Data = bytes,
            ContentType = AttachmentHelper.DetectContentType(bytes),
            Size = AttachmentHelper.GetSize(bytes)
        };

        await attachmentRepository.AddAsync(attachment, cancellationToken);

        return attachment.Id;
    }

    #endregion
}