using Application.Features.Category.Command.Delete;
using Application.Models.Responses;
using MediatR;

namespace Application.Features.Tag.Command.Delete;

public class DeleteTagCommand : IRequest<BaseResponse<DeleteTagCommandResponse>>
{
    public Guid Id { get; set; }
}