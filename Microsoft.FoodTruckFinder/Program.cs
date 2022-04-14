using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.FoodTruckFinder;
using Microsoft.FoodTruckFinder.Common;
using Microsoft.FoodTruckFinder.Register;
using Microsoft.FoodTruckFinder.Search;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddScoped<RegisterService>();
        services.AddScoped<SearchService>();
        services.AddScoped<FinderService>(); //TODO: consider adding interface
        services.AddHttpClient();
        services.AddLogging();
    })
    .UseConsoleLifetime()
    .Build();

using (var serviceScope = host.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;

    try
    {
        var finderService = services.GetRequiredService<FinderService>();
        await finderService.Run(args);
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error Occured");
        return (int)Enumerations.ExitCode.Error;
    }
}

return (int)Enumerations.ExitCode.Success;