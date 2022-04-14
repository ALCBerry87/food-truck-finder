using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.FoodTruckFinder.CLI;
using Microsoft.FoodTruckFinder.CLI.Search;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddScoped<SearchService>();
        services.AddScoped<FinderService>(); //TODO: consider adding interfaces
        services.AddHttpClient();
        services.AddLogging(configure => configure.AddConsole()) //TODO: toggle log level for local vs. "prod"
            .Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Error);
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
    catch (Exception)
    {
        Console.WriteLine($"Unhandled exception occurred");
        return 1;
    }
}

return 0;