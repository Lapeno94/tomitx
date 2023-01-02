namespace Taxually.TechnicalTest.Infrastructure.Queue
{
    public class TaxuallyQueueClient : ITaxuallyQueueClient
    {
        public Task EnqueueAsync<TPayload>(string queueName, TPayload payload)
        {
            // Code to send to message queue removed for brevity
            Console.WriteLine(queueName);
            return Task.CompletedTask;
        }
    }
}