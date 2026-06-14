using Application.Features.Auth.Command.Register;
using Domain.Entities;

namespace Application.Abstractions.Persistence;

public interface ICommentRepository : IRepository<Comment>
{
}