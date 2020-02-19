using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PipelineFailureRepro;

namespace FunctionalTest
{
    public class ApplicationTestFixture : WebApplicationFactory<Program>
    {
        protected override IHostBuilder CreateHostBuilder()
        {
            var builder = base.CreateHostBuilder()
                .ConfigureAppConfiguration((ctx, b) =>
                {
                    b.AddInMemoryCollection(new Dictionary<string, string>
                    {
                        { "DistributedCache:Enabled", false.ToString() }
                    });
                })
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders(); // Remove other loggers
                });

            return builder;
        }
    }
}
