using FlyingDutchmanAirlines.DatabaseLayer.Models;
using FlyingDutchmanAirlines.Exceptions;
using FlyingDutchmanAirlines.RepositoryLayer.Interfaces;
using FlyingDutchmanAirlines.ServiceLayer.Views;

namespace FlyingDutchmanAirlines.ServiceLayer;

public class FlightService : IFlightService
{
    private readonly IFlightRepository _flightRepository;
    private readonly IAirportRepository _airportRepository;

    public FlightService(IFlightRepository flightRepository, IAirportRepository airportRepository)
    {
        _flightRepository = flightRepository;
        _airportRepository = airportRepository;
    }

    public async IAsyncEnumerable<FlightView> GetFlights()
    {
        var flights = _flightRepository.GetFlights();
        foreach (var flight in flights)
        {
            Airport originAirport;
            Airport destinationAirport;

            try
            {
                originAirport = await _airportRepository.GetAirportById(flight.Origin);
                destinationAirport = await _airportRepository.GetAirportById(flight.Destination);
            }
            catch (FlightNotFoundException)
            {
                throw new FlightNotFoundException();
            }
            catch (Exception)
            {
                throw new ArgumentException();
            }

            yield return new FlightView(
                flight.FlightNumber.ToString(),
                (originAirport.City, originAirport.Iata),
                (destinationAirport.City, destinationAirport.Iata)
            );
        }
    }

    public async Task<FlightView> GetFlightByFlightNumber(int flightNumber)
    {
        try
        {
            var flight = await _flightRepository.GetFlightByFlightNumber(flightNumber);
            var originAirport = await _airportRepository.GetAirportById(flight.Origin);
            var destinationAirport = await _airportRepository.GetAirportById(flight.Destination);

            return new FlightView(
                flight.FlightNumber.ToString(),
                (originAirport.City, originAirport.Iata),
                (destinationAirport.City, destinationAirport.Iata)
            );
        }
        catch (FlightNotFoundException)
        {
            throw new FlightNotFoundException();
        }
        catch (Exception)
        {
            throw new ArgumentException();
        }
    }
}
