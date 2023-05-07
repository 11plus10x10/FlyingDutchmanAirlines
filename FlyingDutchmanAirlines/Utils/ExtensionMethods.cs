namespace FlyingDutchmanAirlines.Utils;

internal static class ExtensionMethods
{
    internal static bool IsNullOrContainsSpecialChars(this string str)
    {
        var specialCharacters = new[]
        {
            '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', ',', '.', '?', '"', ':', '{', '}', '|', '<', '>', '=',
            '+', '/', '\\', '-', '_', '[', ']'
        };

        return string.IsNullOrEmpty(str) || str.Any(c => specialCharacters.Contains(c));
    }

    internal static bool IsNegative(this int number) => number < 0;

    internal static bool AnyIsNegative(params int[] numbers) => numbers.Any(n => n.IsNegative());
}
