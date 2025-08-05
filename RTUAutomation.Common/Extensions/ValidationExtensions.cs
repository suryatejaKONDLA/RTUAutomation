namespace RTUAutomation.Common.Extensions;

public static class ValidationExtensions
{
    public static Dictionary<string, string[]> ToErrorDictionary(this IEnumerable<ValidationFailure> failures)
    {
        return failures
            .GroupBy(e => e.PropertyName)
            .ToDictionary(
                g => g.Key,
                g => g.Select(e => e.ErrorMessage).ToArray()
            );
    }
}