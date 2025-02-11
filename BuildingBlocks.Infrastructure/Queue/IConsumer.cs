using RabbitMQ.Client.Events;

namespace BuildingBlocks.Infrastructure.Queue
{
    public interface IConsumer
    {
        Task ConsumeMessageAsync(AsyncEventHandler<BasicDeliverEventArgs> consumEventHandler);
    }
}