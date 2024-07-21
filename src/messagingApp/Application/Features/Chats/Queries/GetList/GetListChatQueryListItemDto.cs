namespace Application.Features.Chats.Queries.GetList;

public class GetListChatQueryListItemDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? ImageIdentifier { get; set; }
    public string? InvitationCode { get; set; }
}

