using FlyingDutchmanAirlines.DatabaseLayer;
using FlyingDutchmanAirlines.DatabaseLayer.Models;
using FlyingDutchmanAirlines.Exceptions;
using FlyingDutchmanAirlines.Utils;

namespace FlyingDutchmanAirlines.RepositoryLayer;

public class BookingRepository : RepositoryBase, IBookingRepository
{
    public BookingRepository(FlyingDutchmanAirlinesContext context) : base(context)
    {
    }

    public async Task CreateBooking(int customerId, int flightNumber)
    {
        if (customerId.IsNegative() || flightNumber.IsNegative())
        {
            throw new ArgumentException("Invalid arguments provided");
        }

        var newBooking = new Booking { CustomerId = customerId, FlightNumber = flightNumber };

        try
        {
            Context.Bookings.Add(newBooking);
            await Context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Exception during database query: {e.Message}");
            throw new CouldNotAddBookingToDatabaseException();
        }
    }
}
