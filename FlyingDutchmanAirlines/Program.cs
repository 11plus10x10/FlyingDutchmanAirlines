using FlyingDutchmanAirlines.DatabaseLayer;
using FlyingDutchmanAirlines.RepositoryLayer;
using FlyingDutchmanAirlines.RepositoryLayer.Interfaces;
using FlyingDutchmanAirlines.ServiceLayer;
using FlyingDutchmanAirlines.ServiceLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<FlyingDutchmanAirlinesContext>(opt =>
    opt.UseSqlServer("Name=ConnectionStrings:FlyingDutchmanAirlines"),
    ServiceLifetime.Transient);
builder.Services.AddTransient<IFlightService, FlightService>();
builder.Services.AddTransient<IFlightRepository, FlightRepository>();
builder.Services.AddTransient<IAirportRepository, AirportRepository>();
builder.Services.AddTransient<IBookingService, BookingService>();
builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
builder.Services.AddTransient<IBookingRepository, BookingRepository>();
//  Delete in final version.
builder.Configuration.AddUserSecrets<Program>();


var app = builder.Build();

app.MapControllers();

app.Run();
