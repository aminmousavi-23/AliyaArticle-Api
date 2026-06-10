using Application.Features.Auth.Command.Login;
using Application.Features.Auth.Command.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController(IMediator mediator) : ControllerBase
{
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
    
    /*[HttpPost("refresh")] TODO: init later
    public async Task<IActionResult> Post([FromBody] RefreshTokenCommand request,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);
        return StatusCode(result.StatusCode, result);
    }
    
    [HttpPost("logout")]
    public async Task<IActionResult> Post([FromBody] LogoutUserCommand request,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);
        return StatusCode(result.StatusCode, result);
    }*/
}