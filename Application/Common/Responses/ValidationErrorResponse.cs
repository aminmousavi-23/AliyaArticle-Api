namespace Application.Common.Responses;

public class ValidationErrorResponse
{
    public string Key { get; set; } = string.Empty;
    public string[] Value { get; set; } = [];
}