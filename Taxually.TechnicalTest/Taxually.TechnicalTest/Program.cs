using Microsoft.AspNetCore;
using Taxually.TechnicalTest;

try
{
    var host = BuildWebHost(args);
    await host.RunAsync();
}
catch (Exception exception)
{
    Console.WriteLine(exception.Message);
}

IWebHost BuildWebHost(string[] args) =>
    WebHost.CreateDefaultBuilder(args)
        .UseStartup<Startup>()
        .Build();