using FlyingDutchman.Tests.Utils;
using FlyingDutchmanAirlines.DatabaseLayer;
using FlyingDutchmanAirlines.DatabaseLayer.Models;
using FlyingDutchmanAirlines.RepositoryLayer;

namespace FlyingDutchman.Tests;

[TestClass]
public class AirportRepositoryTests : RepositoryTester
{
    private FlyingDutchmanAirlinesContext _context;
    private AirportRepository _repository;

    [TestInitialize]
    public async Task TestInitialize()
    {
        _context = GetContext(true);
        await _context.Database.EnsureDeletedAsync();
        var airport = new Airport { AirportId = 0, City = "London", Iata = "LHR" };
        _context.Airports.Add(airport);
        await _context.SaveChangesAsync();
        _repository = new AirportRepository(_context);
        
        Assert.IsNotNull(_repository);
        Assert.AreEqual(0, airport.AirportId);
        Assert.AreEqual("London", airport.City);
        Assert.AreEqual("LHR", airport.Iata);
    }

    [TestMethod]
    public async Task GetAirportByIdSuccess()
    {
        var airport = await _repository.GetAirportById(0);
        Assert.IsNotNull(airport);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public async Task GetAirportByIdFailureInvalidId()
    {
        await using var outputStream = new StringWriter();
        try
        {
            Console.SetOut(outputStream);
            await _repository.GetAirportById(-1);
        }
        catch (ArgumentException)
        {
            Assert.IsTrue(outputStream.ToString().Contains("Negative id provided."));
            throw;
        }
        
    }
}
