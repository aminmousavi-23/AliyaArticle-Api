using Application.Abstractions.Persistence;
using Application.Common.Resources;
using Application.Models.Responses;
using AutoMapper;
using MediatR;

namespace Application.Features.Auth.Queries.GetUserById;

public class GetUserByIdQueryHandler(
    IMapper mapper,
    IUserRepository userRepository ) 
    : IRequestHandler<GetUserByIdQuery, BaseResponse<GetUserByIdQueryResponse>>
{
    public async Task<BaseResponse<GetUserByIdQueryResponse>> Handle(
        GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.Id, cancellationToken);
        if (user == null)
        {
            return ResponseFactory
                .NotFound<GetUserByIdQueryResponse>(Messages.User.NotFound);
        }

        var response = mapper.Map<GetUserByIdQueryResponse>(user);

        return ResponseFactory.Ok(response);
    }
}