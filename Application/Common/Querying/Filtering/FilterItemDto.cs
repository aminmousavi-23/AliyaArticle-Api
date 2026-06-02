namespace Application.Common.Querying.Filtering;

public class FilterItemDto
{
    public string Field { get; set; } = default!;
    public object? Value { get; set; }
    public FilterOperation Operation { get; set; }
}