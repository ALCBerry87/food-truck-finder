# food-truck-finder
.NET-based CLI app for finding SF food trucks

raw notes:

3rd party libraries: 
-CommandLineParser (https://github.com/commandlineparser/commandline)

Dependency injection:
-using it, but may not be worth it for this specific case

versioning?

--soda has .NET library, but it's dependent on .NET Framework 4.5

--design decision: domain driven development, organizing code into logical areas (register / search) instead of by "type"

--save registration info in c:\users\name\.ftf\credentials.json