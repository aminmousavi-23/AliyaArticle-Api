using System.Collections;
using System.Text.Json;

namespace Application.Common.Helpers;

public static class ObjectHelper
{
    public static object? ConvertValue(object? value, Type targetType)
    {
        if (value is null) return null;

        var underlyingType = Nullable.GetUnderlyingType(targetType);
        var effectiveType = underlyingType ?? targetType;

        // Extract value from JsonElement if necessary
        if (value is JsonElement jsonElement)
        {
            if (jsonElement.ValueKind == JsonValueKind.Null) return null;

            value = jsonElement.ValueKind switch
            {
                JsonValueKind.String => jsonElement.GetString(),
                JsonValueKind.Number => GetNumber(jsonElement, effectiveType),
                JsonValueKind.True or JsonValueKind.False => jsonElement.GetBoolean(),
                _ => jsonElement.GetRawText()
            };
        }

        if (value == null) return null;

        // Custom conversions for common types
        if (effectiveType == typeof(Guid))
            return Guid.Parse(value.ToString()!);

        if (effectiveType.IsEnum)
            return Enum.Parse(effectiveType, value.ToString()!, true);

        if (effectiveType == typeof(DateTime))
            return DateTime.Parse(value.ToString()!);

        // Fallback to standard conversion
        return Convert.ChangeType(value, effectiveType);
    }

    public static object ConvertEnumerable(object? value, Type targetElementType)
    {
        // Create a List<T> dynamically
        var listType = typeof(List<>).MakeGenericType(targetElementType);
        var list = (IList)Activator.CreateInstance(listType)!;

        if (value is JsonElement jsonElement && jsonElement.ValueKind == JsonValueKind.Array)
        {
            foreach (var element in jsonElement.EnumerateArray())
            {
                list.Add(ConvertValue(element, targetElementType));
            }
        }
        else if (value is IEnumerable enumerable && value is not string)
        {
            foreach (var item in enumerable)
            {
                list.Add(ConvertValue(item, targetElementType));
            }
        }
        else
        {
            // If it's a single value, wrap it in a list
            list.Add(ConvertValue(value, targetElementType));
        }

        return list;
    }

    private static object GetNumber(JsonElement element, Type targetType)
    {
        if (targetType == typeof(int)) return element.GetInt32();
        if (targetType == typeof(long)) return element.GetInt64();
        if (targetType == typeof(decimal)) return element.GetDecimal();
        if (targetType == typeof(double)) return element.GetDouble();
        if (targetType == typeof(float)) return element.GetSingle();
        return element.GetDecimal(); // Default
    }
}