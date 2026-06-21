
namespace Application.Features.Tag.Command.Create;

public class CreateTagCommand : IRequest<BaseResponse<CreateTagCommandResponse>>
{
    public string Name { get; set; } = default!;
}