using CommandLine;
using Microsoft.FoodTruckFinder.CLI.Search;

namespace Microsoft.FoodTruckFinder.CLI
{
    public class FinderService
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
            var errorsStoppingProcessing = errors.Where(e => e.StopsProcessing);

            //Only show errors if they actually prevent processing of the request
            if (errorsStoppingProcessing.Any())
            {
                foreach (var error in errorsStoppingProcessing)
                {
                    Console.WriteLine(error.ToString());
                }

                return Task.FromResult(1);
            }

            return Task.FromResult(0);
        }
    }
}
