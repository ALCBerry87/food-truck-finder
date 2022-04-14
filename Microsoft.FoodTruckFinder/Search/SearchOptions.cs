using CommandLine;

namespace Microsoft.FoodTruckFinder.Search
{
    [Verb("search")]
    internal class SearchOptions
    {
        [Option("lat", Required = true, HelpText = "Specifies the latitude used for finding nearby food trucks")]
        public decimal Latitude { get; set; }

        [Option("long", Required = true, HelpText = "Specifies the latitude used for finding nearby food trucks")]
        public decimal Longitude { get; set; }

        [Option("radius", Required = false, Default = 20000, HelpText = "Radius of search area in meters. The default is 20,000 (20 KM)")]
        public int RadiusInMeters { get; set; }

        [Option("max-items", Required = false, Default = 5, HelpText = "Specifies the number of search results which will be returned. The default is 5.")]
        public int MaxItems { get; set; }
    }
}
