using RabbitMQ.Client.Events;
using RabbitMQ.Client;

namespace BuildingBlocks.Infrastructure.Queue
{
    public class Consumer(string hostName) : IConsumer
    {
        private readonly ConnectionFactory _factory = new() { HostName = hostName };

        public async Task ConsumeMessageAsync(string queueName, string key, AsyncEventHandler<BasicDeliverEventArgs> consumEventHandler)
        {
            if (_factory is null) throw new InvalidOperationException("Factory is not initialized");
            await using var connection = await _factory.CreateConnectionAsync();
            await using var channel = await connection.CreateChannelAsync();
            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += consumEventHandler;
            await channel.BasicConsumeAsync(queueName, autoAck: true, consumer: consumer);
        }
    }
}
