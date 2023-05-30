using FlyingDutchmanAirlines.DatabaseLayer.Models;

namespace FlyingDutchmanAirlines.ServiceLayer.Interfaces;

public interface IBookingService
{
    Task<(bool, Exception?)> CreateBooking(string customerName, int flightNumber);
    Task<Customer?> GetCustomerFromDatabase(string customerName);
    Task<Customer> AddCustomerToDb(string customerName);
    Task<bool> FlightExistsInDb(int flightNumber);
}
