using Application.Features.User.Queries.GetById;

namespace Application.Abstractions.Infrastructure;

public interface IUserContextAccessor
{
    GetUserByIdQueryResponse GetUserByTokenAsync();
}