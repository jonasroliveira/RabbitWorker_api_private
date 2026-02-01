namespace RabbitWorkerApi.DTOs;

public class MessageDto
{
   public Guid Id { get; init; }
    public string Content { get; init; } = default!;
    public DateTime CreatedAt { get; init; }
    public string Source { get; init; } = default!;
}