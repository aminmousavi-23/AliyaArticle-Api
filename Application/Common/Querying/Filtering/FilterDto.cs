namespace Application.Common.Querying.Filtering;

public class FilterDto
{
    public List<FilterDto> Groups { get; set; } = [];
    public List<FilterItemDto> Items { get; set; } = [];
    public bool IsAnd { get; set; } = true;
    public string? OrderBy { get; set; }
    public bool IsAscending { get; set; } = false;
}