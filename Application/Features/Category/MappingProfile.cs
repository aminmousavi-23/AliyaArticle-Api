using Application.Features.Category.Command.Create;
using Application.Features.Category.Queries.GetPaginated;

namespace Application.Features.Category;

public partial class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateCategoryCommand, Domain.Entities.Category>();
        CreateMap<Domain.Entities.Category, CreateCategoryCommandResponse>();
        
        CreateMap<Domain.Entities.Category, GetCategoryPaginatedQueryResponse>();
    }
}