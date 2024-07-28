namespace WebMVC.Models;

public class HomeViewModel
{
    public List<UserChatViewModel> UserChats { get; set; } = new();
    public List<ChatMessageViewModel> ChatMessages { get; set; } = new();

    public string Nickname { get; set; }
    public Guid UserId { get; set; }
}
