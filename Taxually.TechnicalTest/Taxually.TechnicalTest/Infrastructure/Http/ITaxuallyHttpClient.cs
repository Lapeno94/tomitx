namespace Taxually.TechnicalTest.Infrastructure.Http;

public interface ITaxuallyHttpClient
{
    Task PostAsync<TRequest>(TRequest request);
}