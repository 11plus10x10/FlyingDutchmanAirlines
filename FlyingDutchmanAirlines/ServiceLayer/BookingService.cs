using System.Runtime.ExceptionServices;
using FlyingDutchmanAirlines.DatabaseLayer.Models;
using FlyingDutchmanAirlines.Exceptions;
using FlyingDutchmanAirlines.RepositoryLayer;
using FlyingDutchmanAirlines.RepositoryLayer.Interfaces;
using FlyingDutchmanAirlines.Utils;

namespace FlyingDutchmanAirlines.ServiceLayer;

public class BookingService
{
    private readonly IBookingRepository _bookingBookingRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IFlightRepository _flightRepository;

    public BookingService(IBookingRepository bookingRepository, ICustomerRepository customerRepository,
        IFlightRepository flightRepository)
    {
        _bookingBookingRepository = bookingRepository;
        _customerRepository = customerRepository;
        _flightRepository = flightRepository;
    }

    public async Task<(bool, Exception?)> CreateBooking(string customerName, int flightNumber)
    {
        if (string.IsNullOrEmpty(customerName) || flightNumber.IsNegative())
        {
            return (false, new ArgumentException());
        }

        try
        {
            var customer = await GetCustomerFromDatabase(customerName)
                ?? await AddCustomerToDb(customerName);

            if (!await FlightExistsInDb(flightNumber))
            {
                return (false, new CouldNotAddBookingToDatabaseException());
            }

            await _bookingBookingRepository.CreateBooking(customer.CustomerId, flightNumber);
            return (true, null);
        }
        catch (Exception exception)
        {
            return (false, exception);
        }
    }

    private async Task<Customer?> GetCustomerFromDatabase(string customerName)
    {
        try
        {
            return await _customerRepository.GetCustomerByName(customerName);
        }
        catch (CustomerNotFoundException)
        {
            return null;
        }
        catch (Exception exception)
        {
            ExceptionDispatchInfo.Capture(exception.InnerException ?? new Exception()).Throw();
            return null;
        }
    }

    private async Task<Customer> AddCustomerToDb(string customerName)
    {
        await _customerRepository.CreateCustomer(customerName);
        return await _customerRepository.GetCustomerByName(customerName);
    }

    private async Task<bool> FlightExistsInDb(int flightNumber)
    {
        try
        {
            return await
                _flightRepository.GetFlightByFlightNumber(flightNumber) is not null;
        }
        catch (ArgumentException)
        {
            return false;
        }
        catch (FlightNotFoundException)
        {
            return false;
        }
    }
}
