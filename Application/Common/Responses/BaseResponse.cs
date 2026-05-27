namespace Application.Common.Responses;

public class BaseResponse
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }
    public List<ValidationErrorResponse>? ValidationErrors { get; set; }
    public object? Data { get; set; }
}
public class BaseResponse<T> : BaseResponse
{
    public new T? Data { get; set; }
}

public class CollectionResponse<T> : BaseResponse<List<T>>
{
    public new T? Data { get; set; }
    public long TotalCount { get; set; }
}