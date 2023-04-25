using FlyingDutchmanAirlines.RepositoryLayer;

namespace FlyingDutchman.Tests;

[TestClass]
public class CustomerRepositoryTests
{
    [TestMethod]
    public void CreateCustomerSuccess()
    {
        var repository = new CustomerRepository();
        Assert.IsNotNull(repository);

        var result = repository.CreateCustomer("Fernando Alonso");
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void CreateCustomerFailureNameIsNull()
    {
        var repository = new CustomerRepository();
        Assert.IsNotNull(repository);

        var result = repository.CreateCustomer(null);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void CreateCustomerFailureNameIsEmpty()
    {
        var repository = new CustomerRepository();
        Assert.IsNotNull(repository);

        var result = repository.CreateCustomer(string.Empty);
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
    public void CreateCustomerFailureNameContainsInvalidChars(char invalidChar)
    {
        var repository = new CustomerRepository();
        Assert.IsNotNull(repository);

        var result = repository.CreateCustomer($"Biba{invalidChar}");
        Assert.IsFalse(result);
    }
}
