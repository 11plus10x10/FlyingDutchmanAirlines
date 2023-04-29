using FlyingDutchmanAirlines.DatabaseLayer;
using FlyingDutchmanAirlines.DatabaseLayer.Models;
using FlyingDutchmanAirlines.Exceptions;
using Microsoft.EntityFrameworkCore;

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

    public async Task<Customer> GetCustomerByName(string name)
    {
        if (NameIsInvalid(name))
        {
            throw new CustomerNotFoundException();
        }
        
        return await _context.Customers.FirstOrDefaultAsync(c => c.Name == name)
               ?? throw new CustomerNotFoundException();
    }

    private bool NameIsInvalid(string name)
    {
        var specialCharacters = new[] { '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', ',', '.', '?', '"', ':', '{', '}', '|', '<', '>', '=', '+', '/', '\\', '-', '_', '[', ']' };
        return string.IsNullOrEmpty(name) || name.Any(x => specialCharacters.Contains(x));
    }
}
