using CommandLine;
using Microsoft.FoodTruckFinder.Search;

namespace Microsoft.FoodTruckFinder
{
    internal class FinderService
    {
        private readonly SearchService _searchService;

        public FinderService(SearchService searchService)
        {
            _searchService = searchService;
        }

        public async Task<int> Run(string[] args)
        {
            return await Parser.Default.ParseArguments<SearchOptions>(args)
                .MapResult(
                    (SearchOptions opts) => _searchService.Search(opts),
                    HandleErrors);
        }


        internal Task<int> HandleErrors(IEnumerable<Error> errors)
        {
            Console.WriteLine("Errors occurred during processing:");
            foreach(var error in errors)
            {
                Console.WriteLine(error.ToString());
            }

            return Task.FromResult(1);
        }
    }
}
