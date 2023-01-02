namespace Taxually.TechnicalTest.Infrastructure.Http
{
    public class TaxuallyHttpClient : ITaxuallyHttpClient
    {
        private readonly HttpClient _httpClient;

        public TaxuallyHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public Task PostAsync<TRequest>(TRequest request)
        {
            // Actual HTTP call removed for purposes of this exercise
            Console.WriteLine(_httpClient.BaseAddress);
            return Task.CompletedTask;
        }
    }
}
