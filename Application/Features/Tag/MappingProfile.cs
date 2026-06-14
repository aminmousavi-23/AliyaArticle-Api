using Application.Features.Category.Command.Create;
using Application.Features.Tag.Command.Create;
using Application.Features.Tag.Queries.GetPaginated;
using AutoMapper;

namespace Application.Features.Tag;

public partial class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateTagCommand, Domain.Entities.Tag>();
        CreateMap<Domain.Entities.Tag, CreateTagCommandResponse>();
        
        CreateMap<Domain.Entities.Tag, GetTagPaginatedQueryResponse>();
    }
}