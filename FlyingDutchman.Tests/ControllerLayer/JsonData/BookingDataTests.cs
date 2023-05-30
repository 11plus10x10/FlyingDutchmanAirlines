using FlyingDutchmanAirlines.ControllerLayer.JsonData;

namespace FlyingDutchman.Tests.ControllerLayer.JsonData;

[TestClass]
public class BookingDataTests
{
    [TestMethod]
    public void InstantiationSuccess()
    {
        const string firstName = "Bob";
        const string lastName = "McBob";
        var data = new BookingData { FirstName = firstName, LastName = lastName };
        
        Assert.AreEqual(firstName, data.FirstName);
        Assert.AreEqual(lastName, data.LastName);
    }

    [TestMethod]
    [DataRow("Bob", null)]
    [DataRow(null, "McBob")]
    [ExpectedException(typeof(InvalidOperationException))]
    public void InstantiationFailureNullAsParam(string firstName, string lastName)
    {
        var data = new BookingData { FirstName = firstName, LastName = lastName };
        
        Assert.AreEqual(data.FirstName, firstName);
        Assert.AreEqual(data.LastName, lastName);
    }

    [TestMethod]
    [DataRow("", "McBob")]
    [DataRow("Bob", "")]
    [ExpectedException(typeof(InvalidOperationException))]
    public void InstantiationFailureEmptyStrAsParam(string firstName, string lastName)
    {
        var data = new BookingData { FirstName = firstName, LastName = lastName };
        
        Assert.AreEqual(data.FirstName, firstName);
        Assert.AreEqual(data.LastName, lastName);
    }
}
