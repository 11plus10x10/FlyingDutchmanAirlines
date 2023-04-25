namespace FlyingDutchmanAirlines.RepositoryLayer;

public class CustomerRepository
{
    public bool CreateCustomer(string name)
    {
        return !NameIsInvalid(name);
    }

    private bool NameIsInvalid(string name)
    {
        var specialCharacters = new char[] { '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', ',', '.', '?', '"', ':', '{', '}', '|', '<', '>', '=', '+', '/', '\\', '-', '_', '[', ']' };
        return string.IsNullOrEmpty(name) || name.Any(x => specialCharacters.Contains(x));
    }
}
