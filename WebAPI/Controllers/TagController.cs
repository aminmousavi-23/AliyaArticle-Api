using Application.Features.Tag.Command.Create;
using Application.Features.Tag.Command.Delete;
using Application.Features.Tag.Queries.GetPaginated;
using Infrastructure.Common.Constants;

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

    [Authorize(Roles = RoleTypes.Admin)]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateTagCommand request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);
        return StatusCode(result.StatusCode, result);
    }
    
    [Authorize(Roles = RoleTypes.Admin)]
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