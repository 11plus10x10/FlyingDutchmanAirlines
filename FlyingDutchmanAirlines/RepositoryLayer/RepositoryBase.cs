using FlyingDutchmanAirlines.DatabaseLayer;

namespace FlyingDutchmanAirlines.RepositoryLayer;

public abstract class RepositoryBase
{
    protected readonly FlyingDutchmanAirlinesContext Context;

    protected RepositoryBase(FlyingDutchmanAirlinesContext context)
    {
        Context = context;
    }
}
