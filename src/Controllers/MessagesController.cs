using Microsoft.AspNetCore.Mvc;
using RabbitWorkerApi.Infrastructure.Messaging;

namespace RabbitWorkerApi.Controllers;

[ApiController]
[Route("api/messages")]
public class MessagesController : ControllerBase
{
    private readonly RabbitMqProducer _producer;

    public MessagesController(RabbitMqProducer producer)
    {
        _producer = producer;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] string message)
    {
        await _producer.PublishAsync(message);

        return Accepted();
    }
}