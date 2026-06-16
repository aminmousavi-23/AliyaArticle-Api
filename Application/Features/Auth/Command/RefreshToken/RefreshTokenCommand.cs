using Application.Models.Responses;
using MediatR;

namespace Application.Features.Auth.Command.RefreshToken;

public class RefreshTokenCommand : IRequest<BaseResponse<RefreshTokenCommandResponse>>
{
    public string RefreshToken { get; set; } = default!;
}