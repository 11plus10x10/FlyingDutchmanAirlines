using FlyingDutchmanAirlines.ServiceLayer.Views;

namespace FlyingDutchmanAirlines.ServiceLayer;

public interface IFlightService
{
    IAsyncEnumerable<FlightView> GetFlights();
    Task<FlightView> GetFlightByFlightNumber(int flightNumber);
}