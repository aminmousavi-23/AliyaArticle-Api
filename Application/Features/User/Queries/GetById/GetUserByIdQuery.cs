using Application.Models.Responses;
using MediatR;

namespace Application.Features.User.Queries.GetById;

public class GetUserByIdQuery : IRequest<BaseResponse<GetUserByIdQueryResponse>>
{
    public Guid Id { get; set; }
}