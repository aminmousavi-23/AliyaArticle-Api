using Application.Features.Article.Command.Create;
using Application.Features.Article.Queries.GetById;
using Application.Features.Article.Queries.GetPaginated;

namespace Application.Features.Article;

public partial class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateArticleCommand, Domain.Entities.Article>()
            .ForMember(x => x.Blocks, opt => opt.Ignore());
        CreateMap<Domain.Entities.Article, CreateArticleCommandResponse>();
        
        CreateMap<Domain.Entities.Article, GetArticlePaginatedQueryResponse>();
        CreateMap<Domain.Entities.Article, GetArticleByIdQueryResponse>();
        CreateMap<Domain.Entities.ArticleBlock, GetArticleBlockDto>();
    }
}