using Application.Features.Attachment.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/attachment")]
[ApiController]
public class AttachmentController(IMediator mediator) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new GetAttachmentByIdQuery()
        {
            Id = id
        };
        
        var result = await mediator.Send(request, cancellationToken);
        return File(result.Data!.Data, result.Data.ContentType);
    }
}