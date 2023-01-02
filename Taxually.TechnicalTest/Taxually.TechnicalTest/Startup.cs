using Taxually.TechnicalTest.Application.Services;
using Taxually.TechnicalTest.Infrastructure.Http;
using Taxually.TechnicalTest.Infrastructure.Queue;

namespace Taxually.TechnicalTest;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddHttpClient<ITaxuallyHttpClient, TaxuallyHttpClient>(client =>
        {
            client.BaseAddress = new Uri("https://api.uktax.gov.uk");
        });
        
        services.AddSingleton<ITaxuallyQueueClient, TaxuallyQueueClient>();
        services.AddSingleton<IRegistrationService, RegistrationService>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseHttpsRedirection();

        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.UseRouting();
        
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}