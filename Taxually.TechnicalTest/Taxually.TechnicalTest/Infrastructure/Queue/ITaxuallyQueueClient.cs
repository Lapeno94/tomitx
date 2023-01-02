namespace Taxually.TechnicalTest.Infrastructure.Queue;

public interface ITaxuallyQueueClient
{
    Task EnqueueAsync<TPayload>(string queueName, TPayload payload);
}