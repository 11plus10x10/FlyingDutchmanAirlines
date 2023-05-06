using FlyingDutchmanAirlines.DatabaseLayer;
using FlyingDutchmanAirlines.DatabaseLayer.Models;
using FlyingDutchmanAirlines.Exceptions;
using FlyingDutchmanAirlines.Utils;
using Microsoft.EntityFrameworkCore;

namespace FlyingDutchmanAirlines.RepositoryLayer;

public class AirportRepository : RepositoryBase
{
    public AirportRepository(FlyingDutchmanAirlinesContext context) : base(context)
    {
    }

    public async Task<Airport> GetAirportById(int id)
    {
        if (InputValidator.IdIsInvalid(id)) throw new ArgumentException("Invalid arguments provided.");

        return await Context.Airports.FirstOrDefaultAsync(a => a.AirportId == id)
               ?? throw new AirportNotFoundException();
    }
}
