using RabbitMQ.Client;
using System.Text.Json;

namespace RabbitWorkerApi.Infrastructure.Messaging;

public class RabbitMqProducer : IDisposable
{
    private readonly IConnection _connection;
    private readonly IChannel _channel;

    public RabbitMqProducer()
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost",
            VirtualHost = "/"
        };

        _connection = factory.CreateConnectionAsync().GetAwaiter().GetResult();
        _channel = _connection.CreateChannelAsync().GetAwaiter().GetResult();
        
        _channel.QueueDeclareAsync(
            queue: "message_queue",
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
        ).GetAwaiter().GetResult();
    }

    public async Task PublishAsync<T>(T message)
    {
        var body = JsonSerializer.SerializeToUtf8Bytes(message);

        Console.WriteLine("[API] Publicando:");
        Console.WriteLine(System.Text.Encoding.UTF8.GetString(body));

        await _channel.BasicPublishAsync(
            exchange: "",
            routingKey: "message_queue",
            body: body
        );

        Console.WriteLine("[API] Publicado com sucesso");
    }

    public void Dispose()
    {
        _channel?.Dispose();
        _connection?.Dispose();
    }
}
