using FlyingDutchmanAirlines.DatabaseLayer;
using FlyingDutchmanAirlines.DatabaseLayer.Models;
using FlyingDutchmanAirlines.Exceptions;
using FlyingDutchmanAirlines.Utils;
using Microsoft.EntityFrameworkCore;

namespace FlyingDutchmanAirlines.RepositoryLayer;

public class CustomerRepository : RepositoryBase, ICustomerRepository
{
    public CustomerRepository(FlyingDutchmanAirlinesContext context) : base(context)
    {
    }

    public async Task<bool> CreateCustomer(string name)
    {
        if (name.IsNullOrContainsSpecialChars()) return false;
        try
        {
            var newCustomer = new Customer(name);
            await using (Context)
            {
                Context.Customers.Add(newCustomer);
                await Context.SaveChangesAsync();
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
        if (name.IsNullOrContainsSpecialChars())
        {
            throw new CustomerNotFoundException();
        }

        return await Context.Customers.FirstOrDefaultAsync(c => c.Name == name)
               ?? throw new CustomerNotFoundException();
    }
}
