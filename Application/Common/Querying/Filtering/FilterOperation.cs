namespace Application.Common.Querying.Filtering;

public enum FilterOperation
{
    Equal = 0,
    NotEqual = 1,
    GreaterThan = 2,
    LessThan = 3,
    Contains = 4,
    Include = 5,
    Exclude = 6
}