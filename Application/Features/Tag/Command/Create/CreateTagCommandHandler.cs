using Application.Abstractions.Persistence;
using Application.Common.Helpers;
using Application.Common.Resources;
using Application.Common.Validation;
using Application.Features.Category.Command.Create;
using Application.Models.Responses;
using AutoMapper;
using MediatR;

namespace Application.Features.Tag.Command.Create;

public class CreateTagCommandHandler(
    IMapper mapper,
    IRequestValidator requestValidator,
    ITagRepository tagRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateTagCommand, BaseResponse<CreateTagCommandResponse>>
{
    public async Task<BaseResponse<CreateTagCommandResponse>> Handle(CreateTagCommand request,
        CancellationToken cancellationToken)
    {
        await requestValidator.ValidateAsync(request);
        
        var slug = SlugHelper.GenerateSlug(request.Name);
        
        var tagExists = await tagRepository.ExistsAsync(slug, cancellationToken);
        if (tagExists)
            slug = $"{slug}-{Guid.NewGuid().ToString()[..6]}";

        var newTag = mapper.Map<Domain.Entities.Tag>(request);
        newTag.Slug = slug;

        await tagRepository.AddAsync(newTag, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var response = mapper.Map<CreateTagCommandResponse>(newTag);
        
        return ResponseFactory.Created(response, Messages.Tag.Created);
    }
}