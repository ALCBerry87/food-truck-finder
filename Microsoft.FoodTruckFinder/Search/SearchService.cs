using Microsoft.FoodTruckFinder.Common;
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

        public async Task Search(SearchOptions options)
        {
            //Get API key/secret
            var apiCredentials = LoadApiCredentials();
            if (apiCredentials == null) throw new ArgumentNullException(nameof(apiCredentials));

            //Set up http client and get JSON results
            //TODO: incorporate filtering, sorting, etc.
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", $"{apiCredentials.Id}:{apiCredentials.Secret}");
            var response = await client.GetAsync(Constants.API_PATH);
            response.EnsureSuccessStatusCode();

            //parse json and write results
            var json = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(json)) throw new Exception("No data found");

            var results = JsonConvert.DeserializeObject<List<SearchResult>>(json);
            for (var i = 0; i < results.Count; i++)
            {
                var result = results[i];
                Console.WriteLine($"Record: {result.Address}");
            }
        }

        private ApiCredentials? LoadApiCredentials()
        {
            var credentialsJson = File.ReadAllText(Constants.DEFAULT_CREDENTIALS_PATH);
            return JsonConvert.DeserializeObject<ApiCredentials>(credentialsJson);
        }
    }
}
