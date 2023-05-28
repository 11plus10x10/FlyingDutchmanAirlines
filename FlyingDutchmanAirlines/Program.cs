using FlyingDutchmanAirlines.DatabaseLayer;
using FlyingDutchmanAirlines.RepositoryLayer;
using FlyingDutchmanAirlines.RepositoryLayer.Interfaces;
using FlyingDutchmanAirlines.ServiceLayer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<FlyingDutchmanAirlinesContext>(opt =>
    opt.UseSqlServer("Server=tcp:codelikeacsharppro.database.windows.net,1433;Initial Catalog=FlyingDutchmanAirlines;Persist Security Info=False;User Id=dev;Password=FlyingDutchmanAirlines1972!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"),
    ServiceLifetime.Transient);
builder.Services.AddTransient<IFlightService, FlightService>();
builder.Services.AddTransient<IFlightRepository, FlightRepository>();
builder.Services.AddTransient<IAirportRepository, AirportRepository>();
builder.Environment.EnvironmentName = "Development";


var app = builder.Build();

app.MapControllers();

app.Run();
