using FlyingDutchman.Tests.Utils;
using FlyingDutchmanAirlines.DatabaseLayer;
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
        _context = GetContext();
        await _context.Database.EnsureDeletedAsync();
        _repository = new AirportRepository(_context);
        Assert.IsNotNull(_repository);
    }

    [TestMethod]
    public async Task GetAirportByIdSuccess()
    {
        var airport = await _repository.GetAirportById(0);
        Assert.IsNotNull(airport);
    }
}
