namespace Application.Common.Querying.Filtering;

public class QueryOptionsDto
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public FilterDto? Filter { get; set; }
}
