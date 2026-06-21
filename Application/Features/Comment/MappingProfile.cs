using Application.Features.Comment.Command.Create;
using Application.Features.Comment.Queries.GetPaginated;

namespace Application.Features.Comment;

public partial class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateCommentCommand, Domain.Entities.Comment>();
        CreateMap<Domain.Entities.Comment, CreateCommentCommandResponse>();
        
        CreateMap<Domain.Entities.Comment, GetCommentPaginatedQueryResponse>();
    }
}