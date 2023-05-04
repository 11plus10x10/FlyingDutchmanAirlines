using FlyingDutchmanAirlines.DatabaseLayer;
using FlyingDutchmanAirlines.DatabaseLayer.Models;

namespace FlyingDutchmanAirlines.RepositoryLayer;

public class AirportRepository : RepositoryBase
{
    public AirportRepository(FlyingDutchmanAirlinesContext context) : base(context)
    {
    }

    public async Task<Airport> GetAirportById(int id)
    {
        return new Airport();
    }
}
