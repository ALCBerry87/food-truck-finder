using CommandLine;
using Microsoft.FoodTruckFinder.Register;
using Microsoft.FoodTruckFinder.Search;
using static Microsoft.FoodTruckFinder.Common.Enumerations;

namespace Microsoft.FoodTruckFinder
{
    internal class FinderService
    {
        private readonly RegisterService _registerService;
        private readonly SearchService _searchService;

        public FinderService(RegisterService registerService, SearchService searchService)
        {
            _registerService = registerService;
            _searchService = searchService;
        }

        public async Task Run(string[] args)
        {
            //if missing arguments, read line
            //given lat/long, get from
            await Parser.Default.ParseArguments<SearchOptions>(args)
                .MapResult(
                    //(RegisterOptions opts) => _registerService.Register(opts),
                    (SearchOptions opts) => _searchService.Search(opts),
                    HandleErrors); //TODO: improve error handling
        }

        internal Task HandleErrors(IEnumerable<Error> errors)
        {
            Console.WriteLine("Errors occurred during processing:");
            foreach(var error in errors)
            {
                Console.WriteLine(error.ToString());
            }

            return Task.CompletedTask;
        }
    }
}
