using System.Text;
using FlyingDutchmanAirlines.DatabaseLayer.Models;
using FlyingDutchmanAirlines.Exceptions;
using FlyingDutchmanAirlines.RepositoryLayer.Interfaces;
using FlyingDutchmanAirlines.ServiceLayer;
using Moq;

namespace FlyingDutchman.Tests.ServiceLayer;

[TestClass]
public class FlightServiceTests
{
    private Mock<IAirportRepository> _airportRepository;
    private Mock<IFlightRepository> _flightRepository;

    [TestInitialize]
    public void TestInitialize()
    {
        _airportRepository = new Mock<IAirportRepository>();
        _flightRepository = new Mock<IFlightRepository>();
        
        var flightInDb = new Flight
        {
            FlightNumber = 148,
            Origin = 31,
            Destination = 92,
        };

        var mockReturn = new Queue<Flight>(1);
        mockReturn.Enqueue(flightInDb);

        _flightRepository
            .Setup(repository => repository.GetFlights())
            .Returns(mockReturn);
        
        _flightRepository
            .Setup(repository => repository.GetFlightByFlightNumber(148))
            .Returns(Task.FromResult(flightInDb));
        
        _airportRepository
            .Setup(repository => repository.GetAirportById(31))
            .ReturnsAsync(new Airport { AirportId = 31, City = "Mexico", Iata = "MEX" });

        _airportRepository
            .Setup(repository => repository.GetAirportById(92))
            .ReturnsAsync(new Airport { AirportId = 92, City = "Ulaanbaatar", Iata = "UBN" });
    }

    [TestMethod]
    public async Task GetFlightsSuccess()
    {
        var service = new FlightService(_flightRepository.Object, _airportRepository.Object);

        await foreach (var flightView in service.GetFlights())
        {
            Assert.IsNotNull(flightView);
            Assert.AreEqual(flightView.FlightNumber, "148");
            Assert.AreEqual(flightView.Origin.City, "Mexico");
            Assert.AreEqual(flightView.Origin.Code, "MEX");
            Assert.AreEqual(flightView.Destination.City, "Ulaanbaatar");
            Assert.AreEqual(flightView.Destination.Code, "UBN");
        }
    }

    [TestMethod]
    [ExpectedException(typeof(FlightNotFoundException))]
    public async Task GetFlightsFailureRepositoryException()
    {
        _airportRepository
            .Setup(repository => repository.GetAirportById(31))
            .ThrowsAsync(new FlightNotFoundException());

        var service = new FlightService(_flightRepository.Object, _airportRepository.Object);

        await foreach (var _ in service.GetFlights())
        {
        }
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public async Task GetFlightsFailureRegularException()
    {
        _airportRepository
            .Setup(repository => repository.GetAirportById(31))
            .ThrowsAsync(new NotSupportedException());

        var service = new FlightService(_flightRepository.Object, _airportRepository.Object);

        await foreach (var _ in service.GetFlights())
        {
        }
    }

    [TestMethod]
    public async Task GetFlightByFlightNumberSuccess()
    {
        var service = new FlightService(_flightRepository.Object, _airportRepository.Object);
        var flightView = await service.GetFlightByFlightNumber(148);
        
        Assert.IsNotNull(service);
        Assert.AreEqual(flightView.FlightNumber, "148");
        Assert.AreEqual(flightView.Origin.City, "Mexico");
        Assert.AreEqual(flightView.Origin.Code, "MEX");
        Assert.AreEqual(flightView.Destination.City, "Ulaanbaatar");
        Assert.AreEqual(flightView.Destination.Code, "UBN");
    }

    [TestMethod]
    [ExpectedException(typeof(FlightNotFoundException))]
    public async Task GetFlightByNumberFailureFlightNotFoundException()
    {
        _flightRepository
            .Setup(repository => repository.GetFlightByFlightNumber(606) )
            .ThrowsAsync(new FlightNotFoundException());

        var service = new FlightService(_flightRepository.Object, _airportRepository.Object);
        await service.GetFlightByFlightNumber(606);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public async Task GetFlightByNumberFailureRegularException()
    {
        _flightRepository
            .Setup(repository => repository.GetFlightByFlightNumber(1001))
            .ThrowsAsync(new EncoderFallbackException());

        var service = new FlightService(_flightRepository.Object, _airportRepository.Object);
        await service.GetFlightByFlightNumber(1001);
    }
}
