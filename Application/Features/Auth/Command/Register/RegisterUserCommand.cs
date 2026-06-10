using Application.Models.Responses;
using MediatR;

namespace Application.Features.Auth.Command.Register;

public class RegisterUserCommand : IRequest<BaseResponse<RegisterUserCommandResponse>>
{
    public string Username { get; set; } = default!;
    public string FullName { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string? Email { get; set; }

    public string Password { get; set; } = default!;
    public string ConfirmPassword { get; set; } = default!;
}