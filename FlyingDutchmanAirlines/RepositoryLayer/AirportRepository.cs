using FlyingDutchmanAirlines.DatabaseLayer;
using FlyingDutchmanAirlines.DatabaseLayer.Models;
using FlyingDutchmanAirlines.Exceptions;
using FlyingDutchmanAirlines.RepositoryLayer.Interfaces;
using FlyingDutchmanAirlines.Utils;
using Microsoft.EntityFrameworkCore;

namespace FlyingDutchmanAirlines.RepositoryLayer;

public class AirportRepository : RepositoryBase, IAirportRepository
{
    public AirportRepository(FlyingDutchmanAirlinesContext context) : base(context)
    {
    }

    public async Task<Airport> GetAirportById(int id)
    {
        if (id.IsNegative())
        {
            Console.WriteLine($"Invalid arguments provided. {nameof(id)}={id} is negative");
            throw new ArgumentException();
        }

        return await Context.Airports.FirstOrDefaultAsync(a => a.AirportId == id)
               ?? throw new AirportNotFoundException();
    }
}
