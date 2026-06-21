using Application.Features.Comment.Command.Create;
using Application.Features.Comment.Command.Delete;
using Application.Features.Comment.Queries.GetPaginated;
using Infrastructure.Common.Constants;

namespace WebAPI.Controllers;

[Route("api/comment")]
[ApiController]
public class CommentController(IMediator mediator) : ControllerBase
{
    [HttpPost("search")]
    public async Task<IActionResult> GetPaginated([FromBody] GetCommentPaginatedQuery request,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);
        return StatusCode(result.StatusCode, result);
    }
    
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);
        return StatusCode(result.StatusCode, result);
    }
    
    [Authorize(Roles = RoleTypes.Admin)]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new DeleteCommentCommand()
        {
            Id = id
        };
        
        var result = await mediator.Send(request, cancellationToken);
        return StatusCode(result.StatusCode, result);
    }
}