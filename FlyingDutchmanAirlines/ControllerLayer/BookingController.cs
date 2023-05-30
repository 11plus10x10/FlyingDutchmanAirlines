using System.Net;
using FlyingDutchmanAirlines.ControllerLayer.JsonData;
using FlyingDutchmanAirlines.Exceptions;
using FlyingDutchmanAirlines.ServiceLayer;
using FlyingDutchmanAirlines.ServiceLayer.Interfaces;
using FlyingDutchmanAirlines.Utils;
using Microsoft.AspNetCore.Mvc;

namespace FlyingDutchmanAirlines.ControllerLayer;

[Route("[controller]")]
public class BookingController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }
    
    [HttpPost("{flightNumber:int}")]
    public async Task<IActionResult> CreateBooking([FromBody] BookingData body, int flightNumber)
    {
        if (ModelState.IsValid && !flightNumber.IsNegative())
        {
            var name = $"{body.FirstName} {body.LastName}";
            (bool result, Exception? exception) = await _bookingService.CreateBooking(name, flightNumber);

            if (result && exception is null)
            {
                return StatusCode((int) HttpStatusCode.Created);
            }

            return exception is CouldNotAddBookingToDatabaseException
                ? StatusCode((int) HttpStatusCode.NotFound)
                : StatusCode((int) HttpStatusCode.InternalServerError, exception?.Message);
        }

        return StatusCode((int) HttpStatusCode.InternalServerError, ModelState.Root.Errors.First().ErrorMessage);
    }
}
