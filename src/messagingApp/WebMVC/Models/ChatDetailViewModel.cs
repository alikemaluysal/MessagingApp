namespace WebMVC.Models;

public class ChatDetailViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? ImageIdentifier { get; set; }
    public string? InvitationCode { get; set; }
    public List<ChatUserDto> ChatUsers { get; set; } = new();
}

public class ChatUserDto
{

    public Guid UserId { get; set; }
    public string Nickname { get; set; }
}