namespace Application.Common.Validation;

public interface IRequestValidator
{
    Task ValidateAsync<T>(T request);
}