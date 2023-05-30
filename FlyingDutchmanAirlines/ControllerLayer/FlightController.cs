using System.Net;
using FlyingDutchmanAirlines.Exceptions;
using FlyingDutchmanAirlines.ServiceLayer;
using FlyingDutchmanAirlines.ServiceLayer.Interfaces;
using FlyingDutchmanAirlines.ServiceLayer.Views;
using FlyingDutchmanAirlines.Utils;
using Microsoft.AspNetCore.Mvc;

namespace FlyingDutchmanAirlines.ControllerLayer;

[Route("[controller]")]
public class FlightController : ControllerBase
{
    private readonly IFlightService _flightService;

    public FlightController(IFlightService flightService)
    {
        _flightService = flightService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetFlights()
    {
        try
        {
            var flights = new Queue<FlightView>();
            await foreach (var flight in _flightService.GetFlights())
            {
                flights.Enqueue(flight);
            }

            return StatusCode((int) HttpStatusCode.OK, flights);
        }
        catch (FlightNotFoundException)
        {
            return StatusCode((int) HttpStatusCode.NotFound, "No flights were found in the database");
        }
        catch (Exception)
        {
            return StatusCode((int) HttpStatusCode.InternalServerError, "An error occured");
        }
    }

    [HttpGet("{flightNumber:int}")]
    public async Task<IActionResult> GetFlightByFlightNumber(int flightNumber)
    {
        try
        {
            if (flightNumber.IsNegative()) throw new ArgumentException();

            var flight = await _flightService.GetFlightByFlightNumber(flightNumber);
            return StatusCode((int) HttpStatusCode.OK, flight);
        }
        catch (FlightNotFoundException)
        {
            return StatusCode((int) HttpStatusCode.NotFound, "The flight was not found in the database");
        }
        catch (ArgumentException)
        {
            return StatusCode((int) HttpStatusCode.BadRequest, "Bad request");
        }
        catch (Exception)
        {
            return StatusCode((int) HttpStatusCode.InternalServerError, "An error occured");
        }
    }
}
