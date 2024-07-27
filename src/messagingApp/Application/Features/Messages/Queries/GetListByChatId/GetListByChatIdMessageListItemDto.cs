namespace Application.Features.Messages.Queries.GetListByChatId;

public class GetListByChatIdMessageListItemDto
{
    public Guid Id { get; set; }
    public Guid ChatId { get; set; }
    public Guid UserId { get; set; }
    public string? Content { get; set; }
    public string? FileIdentifier { get; set; }
    public DateTime SentAt { get; set; }
    public string SenderName { get; set; } = default!;

}