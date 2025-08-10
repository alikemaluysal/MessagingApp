namespace Application.Features.Auth.Queries.GetUser;

public class GetUserResponse
{
    public Guid Id { get; set; }
    public string DisplayName { get; set; }
    public string UserName { get; set; }
    public string ProfileImageUrl { get; set; }
    public string Email { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsVerified { get; set; }
}