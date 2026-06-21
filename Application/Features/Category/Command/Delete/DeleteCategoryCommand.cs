
namespace Application.Features.Category.Command.Delete;

public class DeleteCategoryCommand : IRequest<BaseResponse<DeleteCategoryCommandResponse>>
{
    public Guid Id { get; set; }
}