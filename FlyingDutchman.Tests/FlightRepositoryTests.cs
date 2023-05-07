using FlyingDutchman.Tests.Utils;
using FlyingDutchmanAirlines.DatabaseLayer;
using FlyingDutchmanAirlines.DatabaseLayer.Models;
using FlyingDutchmanAirlines.RepositoryLayer;

namespace FlyingDutchman.Tests;

[TestClass]
public class FlightRepositoryTests : RepositoryTester
{
    private FlyingDutchmanAirlinesContext _context;
    private FlightRepository _repository;

    [TestInitialize]
    public void TestInitialize()
    {
        _context = GetContext();
        _repository = new FlightRepository(_context);
        var tstFLight = new Flight { FlightNumber = 1, Origin = 1, Destination = 2 };
        _context.Flights.Add(tstFLight);
        _context.SaveChanges();
        Assert.IsNotNull(_repository);
    }

    [TestMethod]
    public async Task GetFlightByNumberSuccess()
    {
        var flight = await _repository.GetFlightByFlightNumber(1, 1, 2);
        var dbFlight = _context.Flights.FirstOrDefault(f => f.FlightNumber == 1);
        Assert.IsNotNull(dbFlight);
        Assert.AreEqual(flight.FlightNumber, dbFlight.FlightNumber);
        Assert.AreEqual(flight.Origin, dbFlight.Origin);
        Assert.AreEqual(flight.Destination, dbFlight.Destination);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public async Task GetFlightByNumberFailureArgumentException()
    {
        await _repository.GetFlightByFlightNumber(-1, 0 ,1);
    }
}
