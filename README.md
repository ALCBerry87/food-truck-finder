# food-truck-finder
"Food Truck Finder" (FTF) is a .NET-based CLI application that can be used to find nearby food trucks in the San Francisco area, based on data available from https://data.sfgov.org/Economy-and-Community/Mobile-Food-Facility-Permit/rqzj-sfat/data.

### Installation
Download and run the MSI to install on Windows: https://github.com/ALCBerry87/food-truck-finder/blob/main/Microsoft.FoodTruckFinder.Installer/Release/Microsoft.FoodTruckFinder.Installer.msi

Distributions are currently not available on macOS or Linux.

### Available Arguments

* **--lat** - **REQUIRED**. Latitude of geolocation used for finding nearby food trucks.
* **--long** - **REQUIRED**. Longitude of geolocation used for finding nearby food trucks.
* **--radius** - OPTIONAL. Radius in meters that will be searched given the latitude and longitude. The default value is 20,000 (20 KM).
* **--max-items** -- OPTIONAL. Maximum number of results returned from the CLI. The default value is 5.
* **--status** - OPTIONAL. Permit status of the food truck. Valid options include APPROVED, EXPIRED, REQUESTED, and SUSPEND. The default is APPROVED.

In addition, the following arguments are supported out of the box by the underlying [CommandLineParser library](https://github.com/commandlineparser/commandline):

* **--help** - Returns information about the CLI usage
* **--version** - Returns the version of the CLI

### Examples

Example 1: Latitude and Longitude only
```bash
C:\Program Files\AustinC\ftf>Microsoft.FoodTruckFinder.CLI.exe --lat 37.79314862 --long -122.4025671
Here are the closest 5 options:

Name: Think is Good Inc.
Distance: 69.67594338
Address: 430 CALIFORNIA ST
Food: Lobster rolls: crab rolls: lobster burritos: crab burritos: chicken burritos: fish burritos: chicken burritos: poke bowls: soups: chips & soda.

Name: Bonito Poke
Distance: 69.67594338
Address: 430 CALIFORNIA ST
Food: Bonito Poke Bowls & Various Drinks


Name: BOWL'D ACAI, LLC.
Distance: 75.28480484
Address: 451 MONTGOMERY ST
Food: Acai Bowls: Smoothies: Juices


Name: Plaza Garibaldy
Distance: 90.47010568
Address: 475 CALIFORNIA ST
Food: Tacos: burritos: quesadillas


Name: Zuri Food Facilities
Distance: 90.47010568
Address: 475 CALIFORNIA ST
Food: Peruvian Food Served Hot
```

Example 2: Specifying radius and max-items
```bash
C:\Program Files\AustinC\ftf>Microsoft.FoodTruckFinder.CLI.exe --lat 37.8 --long -122.5 --radius 5000 --max-items 1
Here are the closest 1 options:

Name: Eva's Catering
Distance: 2028.5554998
Address: 1199 ORTEGA ST
Food: Cold Truck: Burrito: Corn Dog: Salads: Sandwiches: Quesadilla: Tacos: Fried Rice: Cow Mein: Chinese Rice: Noodle Plates: Soup: Bacon: Eggs: Ham: Avacado: Sausages: Beverages
```

### Design & Initial Thought Process

When first reading the description, I initially deliberated between creating a web front-end or a CLI. The note that the team "spends a lot of time in the shell" and
my predilection toward .NET Core swayed me to creating a CLI application via a .NET Core console application. I had also never created an installer package (my work has been more web & cloud oriented), so it seemed like a great opportunity to learn something new.

After exploring the Socrata Open Data API (SODA) documentation, I discovered a JSON endpoint that could be leveraged in place of pulling the CSV. I also briefly looked into an open-source library supporting [SODA on .NET](https://github.com/CityofSantaMonica/SODA.NET), but after realizing it only supported .NET Framework, shifting my focus solely toward using the JSON API.

I then looked into selecting a framework for CLI facilitation. Specifically, I evaluated the .NET-native [System.CommandLine](https://docs.microsoft.com/en-us/archive/msdn-magazine/2019/march/net-parse-the-command-line-with-system-commandline) and a 3rd-party library called [CommandLineParser](https://github.com/commandlineparser/commandline). I was drawn toward the elegance of CommandLineParser's usage of property and class attributes to allow for easy definition and parsing of the CLI arguments.

I used the default `HostBuilder` to set up DI in the Program.cs. This could have been omitted given the limited feature scope, but I knew at a minimum I'd need to abstract the `HttpClient` for unit tests. Aside from leveraging `IHttpClientFactory`, I did not make use of interfaces in this project given the time constraints and lack of anything else that required abstraction for testing.

Setting up the logic and options for the CLI parsing was fairly straightforward. I originally wanted to create two commands: `search` and `configure`. While `search` obviously solves the main challenge of the assignment, I liked the idea and started down the path of using `configure` to specify an API key ID & secret However, the API doesn't _require_ a registered app key (though you are more likely to be throttled), so I scrapped the idea since it was taking time away from the main focus.

The search functionality was pretty simple; Socrata's API documentation is easy to understand. I particularly liked that they had the ability to calculate and order by distance (for ordering especially, I was initially worried I'd have to pull the entire dataset to order by distance regardless of the `--max-items` passed in). 

One design decision that may come across as confusing relates to building the search query:
```C#
var boundary = new Boundary("location", options.Latitude, options.Longitude, options.RadiusInMeters);
var query = new SearchQuery(Constants.API_PATH, boundary);
query.AddWhere(new WhereOptions()
{
    Limit = options.MaxItems,
    Status = options.Status
});
```
Why isn't the boundary (lat/long) just part of the `WhereOptions`? To facilitate potential use of `SearchQuery` in the future, I wanted to require the boundary for construction. For the non-required options, I wanted to use the builder pattern, which works well for improving readability of the logic for query builder classes (vs. passing 1,000 parameters to a constructor).

Creating the Installer project was much simpler than I expected, even for just setting up a basic MSI. I did look briefly into using the [WiX toolset](https://wixtoolset.org/), but after seeing it would require significant XML configuration, decided to keep it simple for the sake of time.

### Next Steps (i.e., What I Didn't Have Time For)

* Set up automated build & unit testing with GitHub Actions
* Implement more robust error handling
* Refactor pieces of the logic in `SearchService.Search` to be more testable. With the current implementation, I'm not able to test that different option settings work as expected. At a minimum, I'd want to split that current method into two components: one that returns the search results given the options, and anything that shows the results in the console. Would likely have something like the following:
    * `IEnumerable<SearchResult> GetSearchResults(SearchOptions options)`
    * `SearchResultExporter.ExportToConsole(results)`
* Create installer packages for macOS and Linux
