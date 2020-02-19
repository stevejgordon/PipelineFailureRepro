using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace PipelineFailureRepro
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", ctx =>
                {
                    var pipeWriter = ctx.Response.BodyWriter;

                    var memory = pipeWriter.GetMemory(2);

                    memory.Span[0] = (byte)'H';
                    memory.Span[1] = (byte)'i';

                    pipeWriter.Advance(2);

                    ctx.Response.StatusCode = StatusCodes.Status200OK;

                    return Task.CompletedTask;
                });
            });
        }
    }
}
