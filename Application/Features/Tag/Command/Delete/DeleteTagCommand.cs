using Application.Features.Category.Command.Delete;

namespace Application.Features.Tag.Command.Delete;

public class DeleteTagCommand : IRequest<BaseResponse<DeleteTagCommandResponse>>
{
    public Guid Id { get; set; }
}