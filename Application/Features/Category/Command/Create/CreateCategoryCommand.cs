
namespace Application.Features.Category.Command.Create;

public class CreateCategoryCommand : IRequest<BaseResponse<CreateCategoryCommandResponse>>
{
    public string Name { get; set; } = default!;
}