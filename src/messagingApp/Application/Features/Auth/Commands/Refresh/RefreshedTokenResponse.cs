namespace Application.Features.Auth.Commands.Refresh;

public class RefreshedTokenResponse 
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}
