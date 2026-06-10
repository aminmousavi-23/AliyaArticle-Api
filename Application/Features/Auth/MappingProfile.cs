using Application.Features.Auth.Command.Register;
using AutoMapper;

namespace Application.Features.Auth;

public partial class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<RegisterUserCommand, Domain.Entities.User>();
        CreateMap<Domain.Entities.User, RegisterUserCommandResponse>();
    }
}