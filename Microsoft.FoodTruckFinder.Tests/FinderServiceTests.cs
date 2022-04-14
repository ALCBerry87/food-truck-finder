using Microsoft.FoodTruckFinder.CLI;
using Microsoft.FoodTruckFinder.CLI.Search;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Microsoft.FoodTruckFinder.Tests
{
    public class FinderServiceTests
    {
        [Fact]
        public async Task FinderService_GivenValidInput_ReturnsSuccessCode()
        {
            var content = TestSetup.GetSampleContentWithTwoRecords();
            var httpClientFactory = TestSetup.CreateHttpFactory(HttpStatusCode.OK, content);
            var searchService = new SearchService(httpClientFactory);
            var finderService = new FinderService(searchService);
            var args = "search --lat 37.79314862 --long -122.4025671".Split(" ");
            var exitCode = await finderService.Run(args);

            Assert.Equal(0, exitCode);
        }

        [Fact]
        public async Task FinderService_WithNoResults_ReturnsSuccessCode()
        {
            var httpClientFactory = TestSetup.CreateHttpFactory(HttpStatusCode.OK, "[]");
            var searchService = new SearchService(httpClientFactory);
            var finderService = new FinderService(searchService);
            var args = "search --lat 37.79314862 --long -122.4025671".Split(" ");
            var exitCode = await finderService.Run(args);

            Assert.Equal(0, exitCode);
        }

        [Fact]
        public async Task FinderService_WithHttpError_ReturnsErrorCode()
        {
            var httpClientFactory = TestSetup.CreateHttpFactory(HttpStatusCode.BadRequest, "");
            var searchService = new SearchService(httpClientFactory);
            var finderService = new FinderService(searchService);
            var args = "search --lat 37.79314862 --long -122.4025671".Split(" ");
            var exitCode = await finderService.Run(args);

            Assert.Equal(1, exitCode);
        }

        [Fact]
        public async Task FinderService_WithMissingLatitude_ReturnsErrorCode()
        {
            var httpClientFactory = TestSetup.CreateHttpFactory(HttpStatusCode.OK, "[]");
            var searchService = new SearchService(httpClientFactory);
            var finderService = new FinderService(searchService);
            var args = "search --long -122.4025671".Split(" ");
            var exitCode = await finderService.Run(args);

            Assert.Equal(1, exitCode);
        }

        [Fact]
        public async Task FinderService_WithMissingLongitude_ReturnsErrorCode()
        {
            var httpClientFactory = TestSetup.CreateHttpFactory(HttpStatusCode.OK, "[]");
            var searchService = new SearchService(httpClientFactory);
            var finderService = new FinderService(searchService);
            var args = "search --lat 37.79314862".Split(" ");
            var exitCode = await finderService.Run(args);

            Assert.Equal(1, exitCode);
        }
    }
}