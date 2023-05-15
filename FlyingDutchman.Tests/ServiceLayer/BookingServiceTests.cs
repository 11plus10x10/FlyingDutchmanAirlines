using FlyingDutchmanAirlines.DatabaseLayer.Models;
using FlyingDutchmanAirlines.Exceptions;
using FlyingDutchmanAirlines.RepositoryLayer;
using FlyingDutchmanAirlines.RepositoryLayer.Interfaces;
using FlyingDutchmanAirlines.ServiceLayer;
using Moq;

namespace FlyingDutchman.Tests.ServiceLayer;

[TestClass]
public class BookingServiceTests
{
    private Mock<IBookingRepository> _bookingRepository;
    private Mock<ICustomerRepository> _customerRepository;
    private Mock<IFlightRepository> _flightRepository;

    [TestInitialize]
    public void TestInitialize()
    {
        _bookingRepository = new Mock<IBookingRepository>();
        _customerRepository = new Mock<ICustomerRepository>();
        _flightRepository = new Mock<IFlightRepository>();
    }
    
    [TestMethod]
    public async Task CreateBookingSuccess()
    {
        _bookingRepository
            .Setup(repository =>
            repository.CreateBooking(0, 0))
            .Returns(Task.CompletedTask);

        _customerRepository
            .Setup(repository =>
                repository.GetCustomerByName("Jake Mill"))
            .Returns(Task.FromResult(new Customer("Jake Mill") { CustomerId = 0 }));

        _flightRepository
            .Setup(repository => repository.GetFlightByFlightNumber(0))
            .Returns(Task.FromResult(new Flight { FlightNumber = 0 }));

        var service = new BookingService(_bookingRepository.Object, _customerRepository.Object, _flightRepository.Object);

        (bool result, Exception? exception) = await service.CreateBooking("Jake Mill", 0);
        
        
        Assert.IsTrue(result);
        Assert.IsNull(exception);
    }

    [TestMethod]
    [DataRow(null, -1)]
    [DataRow("", 0)]
    [DataRow("Bob Jackson", -100)]
    public async Task CreateBookingFailureArgumentException(string customerName, int flightNumber)
    {
        var service = new BookingService(_bookingRepository.Object, _customerRepository.Object, _flightRepository.Object);

        (bool result, Exception? exception) = await service.CreateBooking(customerName, flightNumber);
        
        Assert.IsFalse(result);
        Assert.IsNotNull(exception);
    }

    [TestMethod]
    public async Task CreateBookingFailureBookingRepositoryCouldNotAddException()
    {
        _bookingRepository
            .Setup(repository => repository.CreateBooking(1, 2))
            .Throws(new CouldNotAddBookingToDatabaseException());

        _customerRepository
            .Setup(repository => repository.GetCustomerByName("Mick Bob"))
            .Returns(Task.FromResult(new Customer("Mick Bob") { CustomerId = 1 }));

        var service = new BookingService(_bookingRepository.Object, _customerRepository.Object, _flightRepository.Object);
        
        (bool result, Exception? exception) = await service.CreateBooking("Mick Bob", 2);
        
        Assert.IsFalse(result);
        Assert.IsNotNull(exception);
        Assert.IsInstanceOfType(exception, typeof(CouldNotAddBookingToDatabaseException));
    }

    [TestMethod]
    public async Task CreateBookingFailureFlightNotInDb()
    {
        _flightRepository
            .Setup(repository => repository.GetFlightByFlightNumber(10))
            .Throws(new FlightNotFoundException());
        
        var service = new BookingService(_bookingRepository.Object, _customerRepository.Object, _flightRepository.Object);

        (bool result, Exception? exception) = await service.CreateBooking("Kim Beam", 10);
        
        Assert.IsFalse(result);
        Assert.IsNotNull(exception);
        Assert.IsInstanceOfType(exception, typeof(CouldNotAddBookingToDatabaseException));
    }
}
