using Application.Features.Auth.Command.Login;
using Application.Features.Auth.Command.RefreshToken;
using Application.Features.Auth.Command.Register;
using Application.Features.Auth.Queries.GetUserById;
using Application.Features.Auth.Queries.GetUserPaginated;
using Infrastructure.Common.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController(IMediator mediator) : ControllerBase
{
    [Authorize(Roles = RoleTypes.Admin)]
    [HttpPost("search")]
    public async Task<IActionResult> GetUserPaginated([FromBody] GetUserPaginatedQuery request,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetUserById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new GetUserByIdQuery()
        {
            Id = id
        };
        
        var result = await mediator.Send(request, cancellationToken);
        return StatusCode(result.StatusCode, result);
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Post([FromBody] RegisterUserCommand request,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);
        return StatusCode(result.StatusCode, result);
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Post([FromBody] LoginUserCommand request,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);
        return StatusCode(result.StatusCode, result);
    }
    
    [HttpPost("refresh-token")]
    public async Task<IActionResult> Post([FromBody] RefreshTokenCommand request,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);
        return StatusCode(result.StatusCode, result);
    }
}