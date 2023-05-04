using FlyingDutchman.Tests.Stubs;
using FlyingDutchmanAirlines.DatabaseLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FlyingDutchman.Tests.Utils;

public class RepositoryTester
{
    private protected FlyingDutchmanAirlinesContext GetContext(bool useStub = false)
    {
        var dbOptBuilder = GetDbOptionsBuilder();
        // var context = new FlyingDutchmanAirlinesContextStub(dbOptBuilder.Options);
        var context = useStub
            ? new FlyingDutchmanAirlinesContext(dbOptBuilder.Options)
            : new FlyingDutchmanAirlinesContextStub(dbOptBuilder.Options);

        return context;
    }
    
    private DbContextOptionsBuilder<FlyingDutchmanAirlinesContext> GetDbOptionsBuilder()
    {
        var dbName = Guid.NewGuid().ToString();
        var serviceProvider = new ServiceCollection()
            .AddEntityFrameworkInMemoryDatabase()
            .BuildServiceProvider();
        var builder = new DbContextOptionsBuilder<FlyingDutchmanAirlinesContext>();
        builder.UseInMemoryDatabase(dbName)
            .UseInternalServiceProvider(serviceProvider);

        return builder;
    }
}
