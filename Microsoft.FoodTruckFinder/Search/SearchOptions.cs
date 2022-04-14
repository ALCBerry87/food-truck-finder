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

        [Option("limit", Default = 5, Max = 20, HelpText = "Specifies the number of search results which will be returned. The default is 5.")]
        public int Limit { get; set; }
    }
}
