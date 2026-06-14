using Application.Features.Tag.Command.Create;
using Application.Features.Tag.Command.Delete;
using Application.Features.Tag.Queries.GetPaginated;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/tag")]
[ApiController]
public class TagController(IMediator mediator) : ControllerBase
{
    [HttpPost("search")]
    public async Task<IActionResult> GetPaginated([FromBody] GetTagPaginatedQuery request,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateTagCommand request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);
        return StatusCode(result.StatusCode, result);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new DeleteTagCommand()
        {
            Id = id
        };
        
        var result = await mediator.Send(request, cancellationToken);
        return StatusCode(result.StatusCode, result);
    }
}