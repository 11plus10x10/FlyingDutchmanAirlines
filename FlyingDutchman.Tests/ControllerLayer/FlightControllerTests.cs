using System.Net;
using FlyingDutchmanAirlines.ControllerLayer;
using FlyingDutchmanAirlines.Exceptions;
using FlyingDutchmanAirlines.ServiceLayer;
using FlyingDutchmanAirlines.ServiceLayer.Views;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FlyingDutchman.Tests.ControllerLayer;

[TestClass]
public class FlightControllerTests
{
    [TestMethod]
    public async Task GetFlightsSuccess()
    {
        var service = new Mock<IFlightService>();
        var returnFlightViews = new List<FlightView>(2)
        {
            new FlightView(
                "1932", 
                ("Groningen", "GRQ"),
                ("Phoenix", "PHX")),
            new FlightView(
                "841",
                ("New York City", "JFK"),
                ("London", "LHR")),
        };
        service
            .Setup(s => s.GetFlights())
            .Returns(FlightViewAsyncGenerator(returnFlightViews));

        async IAsyncEnumerable<FlightView> FlightViewAsyncGenerator(IEnumerable<FlightView> views)
        {
            foreach (var view in views)
            {
                yield return view;
            }
        }


        var controller = new FlightController(service.Object);
        var response = await controller.GetFlights() as ObjectResult;
        
        Assert.IsNotNull(response);
        Assert.AreEqual((int) HttpStatusCode.OK, response.StatusCode);
        
        var content = response.Value as Queue<FlightView>;
        Assert.IsNotNull(content);
        
        Assert.IsTrue(returnFlightViews.All(flight => content.Contains(flight)));
    }

    [TestMethod]
    public async Task GetFlightsFailure404()
    {
        var service = new Mock<IFlightService>();
        service
            .Setup(s => s.GetFlights())
            .Throws(new FlightNotFoundException());

        var controller = new FlightController(service.Object);
        var response = await controller.GetFlights() as ObjectResult;
        
        Assert.IsNotNull(response);
        Assert.AreEqual((int) HttpStatusCode.NotFound, response.StatusCode);
        Assert.AreEqual("No flights were found in the database", response.Value);
    }

    [TestMethod]
    public async Task GetFlightFailure500()
    {
        var service = new Mock<IFlightService>();
        service
            .Setup(s => s.GetFlights())
            .Throws(new ArithmeticException());

        var controller = new FlightController(service.Object);
        var response = await controller.GetFlights() as ObjectResult;
        
        Assert.IsNotNull(response);
        Assert.AreEqual((int) HttpStatusCode.InternalServerError, response.StatusCode);
        Assert.AreEqual("An error occured", response.Value);
    }

    [TestMethod]
    public async Task GetFlightByFlightNumberSuccess()
    {
        var service = new Mock<IFlightService>();
        var returnedFlightView = new FlightView(
            "0",
            ("Lagos","LOS"),
            ("Marrakesh","RAK"));
        service
            .Setup(s => s.GetFlightByFlightNumber(0))
            .Returns(Task.FromResult(returnedFlightView));

        var controller = new FlightController(service.Object);
        var response = await controller.GetFlightByFlightNumber(0) as ObjectResult;
        
        Assert.IsNotNull(response);
        Assert.AreEqual((int) HttpStatusCode.OK, response.StatusCode);

        var content = response.Value as FlightView;
        
        Assert.AreEqual(returnedFlightView, content);
    }

    [TestMethod]
    public async Task GetFlightByNumberFailure404()
    {
        var service = new Mock<IFlightService>();
        service
            .Setup(s => s.GetFlightByFlightNumber(1))
            .Throws(new FlightNotFoundException());

        var controller = new FlightController(service.Object);
        var response = await controller.GetFlightByFlightNumber(1) as ObjectResult;
        
        Assert.IsNotNull(response);
        Assert.AreEqual((int) HttpStatusCode.NotFound, response.StatusCode);
        Assert.AreEqual("The flight was not found in the database", response.Value);
    }

    [TestMethod]
    public async Task GetFlightByNumberFailure500()
    {
        var service = new Mock<IFlightService>();
        service
            .Setup(s => s.GetFlightByFlightNumber(2))
            .Throws(new EndOfStreamException());

        var controller = new FlightController(service.Object);
        var response = await controller.GetFlightByFlightNumber(2) as ObjectResult;
        
        Assert.IsNotNull(response);
        Assert.AreEqual((int) HttpStatusCode.InternalServerError, response.StatusCode);
        Assert.AreEqual("An error occured", response.Value);
    }

    [TestMethod]
    public async Task GetFlightByNumberFailure400()
    {
        var service = new Mock<IFlightService>();
        service
            .Setup(s => s.GetFlightByFlightNumber(-1))
            .Throws(new ArgumentException());

        var controller = new FlightController(service.Object);
        var response = await controller.GetFlightByFlightNumber(-1) as ObjectResult;
        
        Assert.IsNotNull(response);
        Assert.AreEqual((int) HttpStatusCode.BadRequest, response.StatusCode);
        Assert.AreEqual("Bad request", response.Value);
    }
}
