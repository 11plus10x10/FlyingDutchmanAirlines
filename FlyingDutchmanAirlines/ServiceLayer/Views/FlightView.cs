namespace FlyingDutchmanAirlines.ServiceLayer.Views;

public struct AirportInfo
{
    public string City { get; }
    public string Code { get; }

    public AirportInfo((string city, string code) airport)
    {
        City = string.IsNullOrWhiteSpace(airport.city) ?
                "No city found" : airport.city;
        Code = string.IsNullOrWhiteSpace(airport.code) ?
                "No code found" : airport.code;
    }
}
 
public class FlightView
{
    public string FlightNumber { get; }
    public AirportInfo Origin { get; }
    public AirportInfo Destination { get; }

    public FlightView(
        string flightNumber,
        (string city, string code) origin,
        (string city, string code) destination
    )
    {
        FlightNumber = string.IsNullOrWhiteSpace(flightNumber) ?
                "No flight number found" : flightNumber;
        Origin = new AirportInfo(origin);
        Destination = new AirportInfo(destination);
    }
}
