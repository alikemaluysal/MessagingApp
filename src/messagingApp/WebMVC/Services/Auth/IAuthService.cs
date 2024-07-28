namespace WebMVC.Services.Auth;

public interface IAuthService
{
    public Task<TokenResponse> LoginAsync(string email, string password);
    public Task<TokenResponse> Register(string email, string password, string nickname);
    public Task<TokenResponse> RefreshToken(string refreshToken);
}
