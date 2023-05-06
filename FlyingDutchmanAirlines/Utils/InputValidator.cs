namespace FlyingDutchmanAirlines.Utils;

public class InputValidator
{
    public static bool IdIsInvalid(params int[] ids)
    {
        if (ids.Any(i => i < 0))
        {
            Console.WriteLine($"Negative id provided.");
            return true;
        }

        return false;
    }

    public static bool NameIsInvalid(string name)
    {
        var specialCharacters = new[]
        {
            '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', ',', '.', '?', '"', ':', '{', '}', '|', '<', '>', '=',
            '+', '/', '\\', '-', '_', '[', ']'
        };
        return string.IsNullOrEmpty(name) || name.Any(x => specialCharacters.Contains(x));
    }
}
