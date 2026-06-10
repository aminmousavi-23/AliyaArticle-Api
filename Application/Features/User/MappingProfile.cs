using Application.Features.User.Queries.GetById;
using Application.Features.User.Queries.GetPaginated;
using AutoMapper;

namespace Application.Features.User;

public partial class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Domain.Entities.User, GetUserByIdQueryResponse>();
        CreateMap<Domain.Entities.User, GetUserPaginatedQueryResponse>();
    }
}