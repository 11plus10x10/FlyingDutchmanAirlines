using FlyingDutchman.Tests.Utils;
using FlyingDutchmanAirlines.DatabaseLayer;
using FlyingDutchmanAirlines.Exceptions;
using FlyingDutchmanAirlines.RepositoryLayer;

namespace FlyingDutchman.Tests;

[TestClass]
public class BookingRepositoryTests : TestClassBase
{
    private FlyingDutchmanAirlinesContext _context;
    private BookingRepository _repository;

    [TestInitialize]
    public void TestInitialize()
    {
        _context = GetContext();
        _repository = new BookingRepository(_context);
        Assert.IsNotNull(_repository);
    }

    [TestMethod]
    public async Task CreateBookingSuccess()
    {
        await _repository.CreateBooking(1, 0);
        await _context.SaveChangesAsync();
        var booking = _context.Bookings.FirstOrDefault();
        Assert.IsNotNull(booking);
        Assert.AreEqual(1, booking.CustomerId);
        Assert.AreEqual(0, booking.FlightNumber);
    }

    [TestMethod]
    [DataRow(-1, 0)]
    [DataRow(0, -1)]
    [DataRow(-1, -1)]
    [ExpectedException(typeof(ArgumentException))]
    public async Task CreateBookingFailureInvalidInputs(int customerId, int flightNumber)
    {
        await _repository.CreateBooking(customerId, flightNumber);
    }

    [TestMethod]
    [ExpectedException(typeof(CouldNotAddBookingToDatabaseException))]
    public async Task CreateBookingFailureDatabaseError()
    {
        await _repository.CreateBooking(0, 1);
    }
}
