using Application.Features.Article.Command.Create;
using Application.Features.Article.Command.Delete;
using Application.Features.Article.Command.Publish;
using Application.Features.Article.Queries.GetById;
using Application.Features.Article.Queries.GetPaginated;
using Infrastructure.Common.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/article")]
[ApiController]
public class ArticleController(IMediator mediator) : ControllerBase
{
    [HttpPost("search")]
    public async Task<IActionResult> GetPaginated([FromBody] GetArticlePaginatedQuery request,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);
        return StatusCode(result.StatusCode, result);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new GetArticleByIdQuery()
        {
            Id = id
        };
        
        var result = await mediator.Send(request, cancellationToken);
        return StatusCode(result.StatusCode, result);
    }

    [Authorize(Roles = RoleTypes.Admin)]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateArticleCommand request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);
        return StatusCode(result.StatusCode, result);
    }
    
    [Authorize(Roles = RoleTypes.Admin)]
    [HttpPut("{id:guid}/publish")]
    public async Task<IActionResult> Put([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new PublishArticleCommand()
        {
            Id = id
        };
        
        var result = await mediator.Send(request, cancellationToken);
        return StatusCode(result.StatusCode, result);
    }
    
    [Authorize(Roles = RoleTypes.Admin)]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new DeleteArticleCommand()
        {
            Id = id
        };
        
        var result = await mediator.Send(request, cancellationToken);
        return StatusCode(result.StatusCode, result);
    }
}