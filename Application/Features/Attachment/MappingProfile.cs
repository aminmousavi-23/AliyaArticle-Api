using Application.Features.Attachment.Queries.GetById;
using AutoMapper;

namespace Application.Features.Attachment;

public partial class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Domain.Entities.Attachment, GetAttachmentByIdQueryResponse>();
    }
}