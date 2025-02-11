using RabbitMQ.Client.Events;
using RabbitMQ.Client;

namespace BuildingBlocks.Infrastructure.Queue
{
    public class Consumer : IConsumer
    {
        private readonly QueueConfig _config;
        private readonly ConnectionFactory _factory;

        public Consumer(QueueConfig config)
        {
            _config = config;
            _factory = new() { HostName = _config.Host, Port= _config.Port, Password = _config.Password, UserName = _config.UserName };
        }
        

        public async Task ConsumeMessageAsync(AsyncEventHandler<BasicDeliverEventArgs> consumEventHandler)
        {
            await using var connection = await _factory.CreateConnectionAsync();
            await using var channel = await connection.CreateChannelAsync();
            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += consumEventHandler;
            await channel.BasicConsumeAsync(_config.QueueName,  autoAck: true, consumer: consumer);
        }
    }
}
