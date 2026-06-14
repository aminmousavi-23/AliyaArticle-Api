using Application.Abstractions.Persistence;
using Application.Common.Helpers;
using Application.Common.Resources;
using Application.Common.Validation;
using Application.Models.Responses;
using AutoMapper;
using MediatR;

namespace Application.Features.Article.Command.Create;

public class CreateArticleCommandHandler(
    IMapper mapper,
    IRequestValidator requestValidator,
    IArticleRepository articleRepository,
    ICategoryRepository categoryRepository,
    ITagRepository tagRepository,
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

        var newArticle = mapper.Map<Domain.Entities.Article>(request);
        newArticle.Slug = slug;
        newArticle.Tags = tags;

        await articleRepository.AddAsync(newArticle, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var response = mapper.Map<CreateArticleCommandResponse>(newArticle);
        
        return ResponseFactory.Created(response, Messages.Article.Created);
    }
}