using FlyingDutchmanAirlines.DatabaseLayer;
using FlyingDutchmanAirlines.DatabaseLayer.Models;
using FlyingDutchmanAirlines.Exceptions;
using FlyingDutchmanAirlines.Utils;
using Microsoft.EntityFrameworkCore;

namespace FlyingDutchmanAirlines.RepositoryLayer;

public class FlightRepository : RepositoryBase, IFlightRepository
{
    public FlightRepository(FlyingDutchmanAirlinesContext context) : base(context)
    {
    }

    public async Task<Flight> GetFlightByFlightNumber(int flightNumber)
    {
        if (flightNumber.IsNegative())
        {
            throw new ArgumentException("Invalid Argument provided");
        }

        return await Context.Flights.FirstOrDefaultAsync(f => f.FlightNumber == flightNumber)
               ?? throw new FlightNotFoundException();
    }
}
