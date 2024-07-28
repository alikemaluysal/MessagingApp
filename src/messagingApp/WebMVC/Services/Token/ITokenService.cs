namespace WebMVC.Services.Token;

public interface ITokenService
{
    string? GetAccessToken();
    string? GetRefreshToken();
    void SetAccessToken(string accessToken);
    void SetRefreshToken(string refreshToken);
}
