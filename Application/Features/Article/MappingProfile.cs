using Application.Features.Article.Command.Create;
using Application.Features.Article.Queries.GetById;
using Application.Features.Article.Queries.GetPaginated;
using AutoMapper;

namespace Application.Features.Article;

public partial class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateArticleCommand, Domain.Entities.Article>();
        CreateMap<Domain.Entities.Article, CreateArticleCommandResponse>();
        
        CreateMap<Domain.Entities.Article, GetArticlePaginatedQueryResponse>();
        CreateMap<Domain.Entities.Article, GetArticleByIdQueryResponse>();
    }
}