using RabbitMQ.Client.Events;

namespace BuildingBlocks.Infrastructure.Queue
{
    public interface IConsumer
    {
        Task ConsumeMessageAsync(string queueName, string key, AsyncEventHandler<BasicDeliverEventArgs> consumEventHandler);
    }
}