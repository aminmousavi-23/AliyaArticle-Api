using Application.Features.User.Command.Register;
using Application.Features.User.Queries.GetById;
using Application.Features.User.Queries.GetPaginated;
using AutoMapper;

namespace Application.Features.User;

public partial class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<RegisterUserCommand, Domain.Entities.User>();
        CreateMap<Domain.Entities.User, RegisterUserCommandResponse>();
        
        CreateMap<Domain.Entities.User, GetUserByIdQueryResponse>();
        CreateMap<Domain.Entities.User, GetUserPaginatedQueryResponse>();
    }
}