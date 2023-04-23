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
    }
}
