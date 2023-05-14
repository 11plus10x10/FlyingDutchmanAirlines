namespace FlyingDutchmanAirlines.RepositoryLayer;

public interface IBookingRepository
{
    Task CreateBooking(int customerId, int flightNumber);
}
