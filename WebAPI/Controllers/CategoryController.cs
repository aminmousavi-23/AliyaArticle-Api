using Application.Features.Category.Command.Create;
using Application.Features.Category.Command.Delete;
using Application.Features.Category.Queries.GetPaginated;
using Infrastructure.Common.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/category")]
[ApiController]
public class CategoryController(IMediator mediator) : ControllerBase
{
    [HttpPost("search")]
    public async Task<IActionResult> GetPaginated([FromBody] GetCategoryPaginatedQuery request,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);
        return StatusCode(result.StatusCode, result);
    }

    [Authorize(Roles = RoleTypes.Admin)]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);
        return StatusCode(result.StatusCode, result);
    }
    
    [Authorize(Roles = RoleTypes.Admin)]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new DeleteCategoryCommand()
        {
            Id = id
        };
        
        var result = await mediator.Send(request, cancellationToken);
        return StatusCode(result.StatusCode, result);
    }
}