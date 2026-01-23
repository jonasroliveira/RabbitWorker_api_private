namespace RabbitWorkerApi.DTOs;

public class SendMessageRequestDto
{
    public Guid Id { get; init; }
    public string Content { get; init; } = default!;
    public DateTime CreatedAt { get; init; }
}