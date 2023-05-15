using FlyingDutchmanAirlines.ServiceLayer.Views;

namespace FlyingDutchman.Tests.ServiceLayer;

[TestClass]
public class FlightViewTests
{
    [TestMethod]
    public void ConstructorSuccess()
    {
        var flightNumber = "0";
        var originCity = "Amsterdam";
        var originCityCode = "AMS";
        var destinationCity = "London";
        var destinationCityCode = "LHR";

        var view = new FlightView(
                flightNumber,
                (originCity, originCityCode),
                (destinationCity, destinationCityCode)
            );
        Assert.IsNotNull(view);
        
        Assert.AreEqual(view.FlightNumber, flightNumber);
        Assert.AreEqual(view.Origin.City, originCity);
        Assert.AreEqual(view.Origin.Code, originCityCode);
        Assert.AreEqual(view.Destination.City, destinationCity);
        Assert.AreEqual(view.Destination.Code, destinationCityCode);
    }

    [TestMethod]
    public void ConstructorSuccessNullOrEmptyCheck()
    {
        var flightNumber = string.Empty;
        var originCity = " ";
        var originCityCode = "\n";
        var destinationCity = "Madrid";
        var destinationCityCode = "";

        var view = new FlightView(
                flightNumber,
                (originCity, originCityCode),
                (destinationCity, destinationCityCode)
            );
        
        Assert.IsNotNull(view);
        Assert.AreEqual(view.FlightNumber, "No flight number found");
        Assert.AreEqual(view.Origin.City, "No city found");
        Assert.AreEqual(view.Origin.Code, "No code found");
        Assert.AreEqual(view.Destination.City, destinationCity);
        Assert.AreEqual(view.Destination.Code, "No code found");
    }
}
