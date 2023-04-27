using FlyingDutchmanAirlines.DatabaseLayer;
using FlyingDutchmanAirlines.RepositoryLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FlyingDutchman.Tests;

[TestClass]
public class CustomerRepositoryTests
{
    private FlyingDutchmanAirlinesContext _context;
    private CustomerRepository _repository;

    [TestInitialize]
    public void TestInitialize()
    {

        var dbOptBuilder = GetDbOptionsBuilder();
        _context = new FlyingDutchmanAirlinesContext(dbOptBuilder.Options);

        _repository = new CustomerRepository(_context);
        Assert.IsNotNull(_repository);
    }

    private static DbContextOptionsBuilder<FlyingDutchmanAirlinesContext> GetDbOptionsBuilder()
    {
        var dbName = Guid.NewGuid().ToString();
        var serviceProvider = new ServiceCollection()
            .AddEntityFrameworkInMemoryDatabase()
            .BuildServiceProvider();
        var builder = new DbContextOptionsBuilder<FlyingDutchmanAirlinesContext>();
        builder.UseInMemoryDatabase(dbName)
            .UseInternalServiceProvider(serviceProvider);

        return builder;
    }

    [TestMethod]
    public async Task CreateCustomerSuccess()
    {
        var result = await _repository.CreateCustomer("Fernando Alonso");
        Assert.IsTrue(result);
    }

    [TestMethod]
    public async Task CreateCustomerFailureNameIsNull()
    {
        var result = await _repository.CreateCustomer(null);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public async Task CreateCustomerFailureNameIsEmpty()
    {
        var result = await _repository.CreateCustomer(string.Empty);
        Assert.IsFalse(result);
    }

    [TestMethod]
    [DataRow('!')]
    [DataRow('@')]
    [DataRow('#')]
    [DataRow('$')]
    [DataRow('%')]
    [DataRow('^')]
    [DataRow('&')]
    [DataRow('*')]
    [DataRow('(')]
    [DataRow(')')]
    [DataRow(',')]
    [DataRow('.')]
    [DataRow('?')]
    [DataRow('"')]
    [DataRow(':')]
    [DataRow('{')]
    [DataRow('}')]
    [DataRow('|')]
    [DataRow('<')]
    [DataRow('>')]
    [DataRow('[')]
    [DataRow(']')]
    [DataRow('_')]
    [DataRow('-')]
    [DataRow('+')]
    [DataRow('=')]
    [DataRow('/')]
    [DataRow('\\')]
    public async Task CreateCustomerFailureNameContainsInvalidChars(char invalidChar)
    {
        var result = await _repository.CreateCustomer($"Biba{invalidChar}");
        Assert.IsFalse(result);
    }

    [TestMethod]
    public async Task CreateCustomerFailureDatabaseAccessError()
    {
        var repository = new CustomerRepository(null);
        
        var result = await repository.CreateCustomer("Willy Wonka");
        Assert.IsFalse(result);
    }
}
