using Microsoft.AspNetCore.Mvc.Testing;

namespace Taxually.TechnicalTest.IntegrationTests;

public class CustomWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseStartup<Startup>().ConfigureServices(services =>
        {
        });

        builder.UseEnvironment("Development");
    }
}