using Application.Abstractions.Persistence;
using Application.Common.Helpers;
using Application.Common.Resources;
using Application.Common.Validation;
using Application.Models.Responses;
using AutoMapper;
using MediatR;

namespace Application.Features.Category.Command.Create;

public class CreateCategoryCommandHandler(
    IMapper mapper,
    IRequestValidator requestValidator,
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateCategoryCommand, BaseResponse<CreateCategoryCommandResponse>>
{
    public async Task<BaseResponse<CreateCategoryCommandResponse>> Handle(CreateCategoryCommand request,
        CancellationToken cancellationToken)
    {
        await requestValidator.ValidateAsync(request);
        
        var slug = SlugHelper.GenerateSlug(request.Name);
        
        var categoryExists = await categoryRepository.ExistsAsync(slug, cancellationToken);
        if (categoryExists)
            slug = $"{slug}-{Guid.NewGuid().ToString()[..6]}";

        var newCategory = mapper.Map<Domain.Entities.Category>(request);
        newCategory.Slug = slug;

        await categoryRepository.AddAsync(newCategory, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var response = mapper.Map<CreateCategoryCommandResponse>(newCategory);
        
        return ResponseFactory.Created(response, Messages.Category.Created);
    }
}