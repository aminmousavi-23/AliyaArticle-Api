using Application.Features.User.Command.Register;
using AutoMapper;

namespace Application.Features.User;

public partial class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<RegisterUserCommand, Domain.Entities.User>();
        CreateMap<Domain.Entities.User, RegisterUserCommandResponse>();
    }
}