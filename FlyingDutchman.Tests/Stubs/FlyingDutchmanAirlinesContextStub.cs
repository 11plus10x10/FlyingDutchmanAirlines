using FlyingDutchmanAirlines.DatabaseLayer;
using FlyingDutchmanAirlines.DatabaseLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FlyingDutchman.Tests.Stubs;

public class FlyingDutchmanAirlinesContextStub : FlyingDutchmanAirlinesContext
{
    public FlyingDutchmanAirlinesContextStub(DbContextOptions<FlyingDutchmanAirlinesContext> options) : base(options)
        => base.Database.EnsureDeleted();
    
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var pendingChanges = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added);
        var bookings = pendingChanges
            .Select(e => e.Entity).OfType<Booking>();
        if (bookings.Any(b => b.CustomerId != 1)) throw new Exception("Database Error!");
        base.SaveChangesAsync(cancellationToken);

        return Task.FromResult(1);
    }
}
