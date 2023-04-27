using FlyingDutchmanAirlines.DatabaseLayer;
using FlyingDutchmanAirlines.DatabaseLayer.Models;

namespace FlyingDutchmanAirlines.RepositoryLayer;

public class CustomerRepository
{
    private readonly FlyingDutchmanAirlinesContext _context;

    public CustomerRepository(FlyingDutchmanAirlinesContext context)
    {
        _context = context;
    }
    
    public async Task<bool> CreateCustomer(string name)
    {
        if (NameIsInvalid(name)) return false;
        try
        {
            var newCustomer = new Customer(name);
            await using (_context)
            {
                _context.Customers.Add(newCustomer);
                await _context.SaveChangesAsync();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }

        return true;
    }

    private bool NameIsInvalid(string name)
    {
        var specialCharacters = new[] { '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', ',', '.', '?', '"', ':', '{', '}', '|', '<', '>', '=', '+', '/', '\\', '-', '_', '[', ']' };
        return string.IsNullOrEmpty(name) || name.Any(x => specialCharacters.Contains(x));
    }
}
