namespace Application.Features.Messages.Queries.GetChatMessages;

public class ChatInfoDto
{
    public Guid UserId { get; set; }
    public Guid ChatId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? ImageUrl { get; set; } = string.Empty;
    public bool IsGroup { get; set; }
    public int ParticipantsCount { get; set; }

    public UserInfoDto UserInfoDto { get; set; } = new();
    public ChannelInfoDto ChannelInfoDto { get; set; } = new();

}

public class UserInfoDto
{
    public Guid UserId { get; set; }
    public string DisplayName { get; set; }
    public string UserName { get; set; }
    public string EmailAddress { get; set; }
    public string? ProfileImageUrl { get; set; } = string.Empty;
}

public class ChannelInfoDto
{
    public List<UserInfoDto> Users { get; set; }
}