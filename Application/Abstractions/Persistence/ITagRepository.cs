using Application.Features.Auth.Command.Register;
using Domain.Entities;

namespace Application.Abstractions.Persistence;

public interface ITagRepository : IRepository<Tag>
{
    Task<IList<Tag>> GetByIdsAsync(IList<Guid> ids, CancellationToken cancellationToken);
}