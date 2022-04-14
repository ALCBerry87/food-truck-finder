using Microsoft.FoodTruckFinder.Tests.Mocks;
using Moq;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Microsoft.FoodTruckFinder.Tests
{
    internal static class TestSetup
    {
        public static string GetSampleContentWithTwoRecords()
        {
            var path = Path.Join(Directory.GetCurrentDirectory(), "\\data\\sampledata.json");
            return File.ReadAllText(path);
        }

        public static IHttpClientFactory CreateHttpFactory(HttpStatusCode httpStatusCode, string httpContent)
        {
            var clientHandlerStub = new DelegatingHandlerStub((request, cancellationToken) => {
                var response = new HttpResponseMessage() { StatusCode = httpStatusCode, Content = new StringContent(httpContent) };
                return Task.FromResult(response);
            });

            var mockHttpFactory = new Mock<IHttpClientFactory>();
            mockHttpFactory.Setup(m => m.CreateClient(It.IsAny<string>()))
            .Returns(() => new HttpClient(clientHandlerStub));

            return mockHttpFactory.Object;
        }
    }
}
