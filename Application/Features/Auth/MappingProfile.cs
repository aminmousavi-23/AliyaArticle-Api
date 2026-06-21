using Application.Features.Auth.Command.Register;
using Application.Features.Auth.Queries.GetUserById;
using Application.Features.Auth.Queries.GetUserPaginated;

namespace Application.Features.Auth;

public partial class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Domain.Entities.User, GetUserByIdQueryResponse>();
        CreateMap<Domain.Entities.User, GetUserPaginatedQueryResponse>();
        
        CreateMap<RegisterUserCommand, Domain.Entities.User>();
        CreateMap<Domain.Entities.User, RegisterUserCommandResponse>();
    }
}