namespace Application.Features.Auth.Commands.Login;

//DTO
public class LoggedInCommandResponse
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsVerified { get; set; }
    public string? ProfileImageUrl { get; set; }
}

