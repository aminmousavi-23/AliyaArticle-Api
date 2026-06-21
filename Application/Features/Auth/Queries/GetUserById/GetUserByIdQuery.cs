
namespace Application.Features.Auth.Queries.GetUserById;

public class GetUserByIdQuery : IRequest<BaseResponse<GetUserByIdQueryResponse>>
{
    public Guid Id { get; set; }
}