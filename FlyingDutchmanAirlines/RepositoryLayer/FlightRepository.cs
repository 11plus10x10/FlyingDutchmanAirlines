using FlyingDutchmanAirlines.DatabaseLayer;
using FlyingDutchmanAirlines.DatabaseLayer.Models;
using FlyingDutchmanAirlines.Exceptions;
using FlyingDutchmanAirlines.Utils;
using Microsoft.EntityFrameworkCore;

namespace FlyingDutchmanAirlines.RepositoryLayer;

public class FlightRepository : RepositoryBase
{
    public FlightRepository(FlyingDutchmanAirlinesContext context) : base(context)
    {
    }

    public async Task<Flight> GetFlightByFlightNumber(int flightNumber, int originAirportId, int destinationAirportId)
    {
        if (ExtensionMethods.AnyIsNegative(flightNumber, originAirportId, destinationAirportId))
        {
            throw new ArgumentException("Invalid Arguments provided");
        }

        return await Context.Flights.FirstOrDefaultAsync(f => f.FlightNumber == flightNumber)
               ?? throw new FlightNotFoundException();
    }
}
