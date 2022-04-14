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

### Design & Initial Thought Process



raw notes:

-tradeoffs
-rationales
-things i would do differently with more time

3rd party libraries: 
-CommandLineParser (https://github.com/commandlineparser/commandline)

Dependency injection:
-using it, but may not be worth it for this specific case

versioning?

--soda has .NET library, but it's dependent on .NET Framework 4.5

--design decision: domain driven development, organizing code into logical areas (register / search) instead of by "type"
