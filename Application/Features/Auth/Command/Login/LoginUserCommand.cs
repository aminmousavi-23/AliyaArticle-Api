using Application.Models.Responses;
using MediatR;

namespace Application.Features.Auth.Command.Login;

public class LoginUserCommand : IRequest<BaseResponse<LoginUserCommandResponse>>
{
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
}