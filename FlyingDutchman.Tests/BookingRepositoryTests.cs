using FlyingDutchman.Tests.Utils;
using FlyingDutchmanAirlines.DatabaseLayer;
using FlyingDutchmanAirlines.RepositoryLayer;

namespace FlyingDutchman.Tests;

[TestClass]
public class BookingRepositoryTests : RepositoryTester
{
    private FlyingDutchmanAirlinesContext _context;
    private BookingRepository _repository;

    [TestInitialize]
    public async Task TestInitialize()
    {
        _context = GetContext();
        _repository = new BookingRepository(_context);
        Assert.IsNotNull(_repository);
    }
    
    [TestMethod]
    public void CreateBookingSuccess() { }

    [TestMethod]
    [DataRow(-1, 0)]
    [DataRow(0, -1)]
    [DataRow(-1, -1)]
    [ExpectedException(typeof(ArgumentException))]
    public async Task CreateBookingFailureInvalidInputs(int customerId, int flightNumber)
    {
        await _repository.CreateBooking(customerId, flightNumber);
    }
}
