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
        var tstFLight = new Flight { FlightNumber = 0, Origin = 0, Destination = 0 };
        _context.Flights.Add(tstFLight);
        _context.SaveChanges();
        Assert.IsNotNull(_repository);
    }

    [TestMethod]
    public async Task GetFlightByNumberSuccess()
    {
        var flight = await _repository.GetFlightByFlightNumber(0);
        var flightInDb = _context.Flights.FirstOrDefault(f => f.FlightNumber == 0);
        Assert.IsNotNull(flightInDb);
        Assert.AreEqual(flight.FlightNumber, flightInDb.FlightNumber);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public async Task GetFlightByNumberFailureArgumentException()
    {
        await _repository.GetFlightByFlightNumber(-1);
    }
}
