using RabbitMQ.Client;
using System.Text;

namespace BuildingBlocks.Infrastructure.Queue;

public class Sender : ISender
{
    private readonly QueueConfig _config;
    private readonly ConnectionFactory _factory;

    public Sender(QueueConfig config)
    {
        _config = config;
        _factory = new() { HostName = _config.Host, Port = _config.Port, Password = _config.Password, UserName = _config.UserName };
    }
    public async Task SendMessageAsync(string message)
    {
        if (_factory is null) throw new InvalidOperationException("Factory is not initialized");
        await using var connection = await _factory.CreateConnectionAsync();
        await using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(queue: _config.QueueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var body = Encoding.UTF8.GetBytes(message);
        await channel.BasicPublishAsync(exchange: string.Empty, routingKey: _config.Key, body: body);
    }

}
