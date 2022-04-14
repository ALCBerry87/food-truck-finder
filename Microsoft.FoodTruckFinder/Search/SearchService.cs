using Microsoft.FoodTruckFinder.Common;
using Microsoft.FoodTruckFinder.Search.QueryOptions;
using Newtonsoft.Json;

namespace Microsoft.FoodTruckFinder.Search
{
    internal class SearchService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public SearchService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<int> Search(SearchOptions options)
        {
            //Build api path with query
            var boundary = new Boundary("location", options.Latitude, options.Longitude, options.RadiusInMeters);
            var query = new SearchQuery(Constants.API_PATH, boundary);
            query.AddWhere(new WhereOptions()
            {
                Limit = options.MaxItems,
                Status = "APPROVED" //TODO: don't hard-code this, add as another parameter
            });

            //Set up http client and get JSON results
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(query.Build());
            response.EnsureSuccessStatusCode();

            //parse json and write results
            var json = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(json)) throw new Exception("API response is empty");

            var results = JsonConvert.DeserializeObject<List<SearchResult>>(json);
            //check for no results
            if (results == null || !results.Any())
            {
                Console.WriteLine("No nearby results found. Try expanding your radius.");
                return 0;
            }

            Console.WriteLine($"Here are the closest {results.Count} options:");
            foreach(var result in results)
            {
                Console.WriteLine($"Name: {result.Applicant}\nDistance: {result.Distance}\nAddress{result.Address}\n");
            }

            return 0;
        }
    }
}
