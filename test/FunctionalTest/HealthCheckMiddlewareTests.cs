using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace FunctionalTest
{
    public class HealthCheckMiddlewareTests : IClassFixture<ApplicationTestFixture>
    {
        private readonly HttpClient _client;

        public HealthCheckMiddlewareTests(ApplicationTestFixture fixture)
        {
            _client = fixture.CreateClient();
        }

        [Fact]
        public async Task HealthCheckMiddleware_ReturnsOkStatusCode()
        {
            var result = await _client.GetAsync("/");

            result.EnsureSuccessStatusCode();
        }
    }
}
