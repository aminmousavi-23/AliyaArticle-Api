using Application.Features.User.Queries.GetById;
using Application.Features.User.Queries.GetPaginated;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/user")]
[ApiController]
public class UserController(IMediator mediator) : ControllerBase
{
    [HttpPost("search")]
    public async Task<IActionResult> GetPaginated([FromBody] GetUserPaginatedQuery request,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new GetUserByIdQuery()
        {
            Id = id
        };
        
        var result = await mediator.Send(request, cancellationToken);
        return StatusCode(result.StatusCode, result);
    }
}