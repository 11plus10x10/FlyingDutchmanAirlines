using FlyingDutchmanAirlines.DatabaseLayer;
using FlyingDutchmanAirlines.DatabaseLayer.Models;
using FlyingDutchmanAirlines.Exceptions;

namespace FlyingDutchmanAirlines.RepositoryLayer;

public class BookingRepository : RepositoryBase
{
    public BookingRepository(FlyingDutchmanAirlinesContext context) : base(context)
    {
    }
    public async Task CreateBooking(int customerId, int flightNumber)
    {
        if (customerId < 0 || flightNumber < 0)
        {
            Console.WriteLine(
                $"Argument exception in CreateBooking! CustomerId = {customerId}, flightNumber = {flightNumber}");
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
