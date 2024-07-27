namespace Application.Features.Messages.Commands.SendMessage;

public class SentMessageResponse
{
    public Guid Id { get; set; }
    public Guid ChatId { get; set; }
    public Guid UserId { get; set; }
    public string? Content { get; set; }
    public string? FileIdentifier { get; set; }
    public DateTime SentAt { get; set; }
}