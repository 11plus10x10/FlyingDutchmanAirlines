using FlyingDutchmanAirlines;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

InitializeHost();

static void InitializeHost() =>
Host.CreateDefaultBuilder()
    .ConfigureWebHostDefaults(builder =>
    {
        builder.UseStartup<Startup>();
        builder.UseUrls("http://0.0.0.0:8080");
    }).Build().Run();
