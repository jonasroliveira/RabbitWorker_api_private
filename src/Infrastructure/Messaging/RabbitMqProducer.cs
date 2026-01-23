using RabbitMQ.Client;
using System.Text;

namespace RabbitWorkerApi.Infrastructure.Messaging;

public class RabbitMqProducer
{
    private readonly IConnection _connection;
    private readonly IChannel _channel;

    public RabbitMqProducer()
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost"
        };

        _connection = factory
            .CreateConnectionAsync()
            .GetAwaiter()
            .GetResult();

        _channel = _connection
            .CreateChannelAsync()
            .GetAwaiter()
            .GetResult();

        _channel.QueueDeclareAsync(
            queue: "message_queue",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null
        ).GetAwaiter().GetResult();
    }

    public async Task PublishAsync(string message)
    {
        var body = Encoding.UTF8.GetBytes(message);

        await _channel.BasicPublishAsync(
            exchange: "",
            routingKey: "message_queue",
            body: body);
    }
}
