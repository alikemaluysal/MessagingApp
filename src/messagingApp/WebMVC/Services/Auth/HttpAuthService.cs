using WebMVC.Util.ExceptionHandling;

namespace WebMVC.Services.Auth;

public class HttpAuthService(IHttpClientFactory httpClientFactory) : IAuthService
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("ApiClient");

    public async Task<TokenResponse> LoginAsync(string email, string password)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/Auth/Login", new { email, password });
        await response.EnsureSuccessStatusCodeWithApiError();
        var result = await response.Content.ReadFromJsonAsync<TokenResponse>();
        return result;

    }

    public async Task<TokenResponse> Register(string email, string password, string nickname)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/Auth/Register", new { email, password, nickname });
        await response.EnsureSuccessStatusCodeWithApiError();
        var result = await response.Content.ReadFromJsonAsync<TokenResponse>();
        return result;
    }

    public async Task<TokenResponse> RefreshToken(string refreshToken)
    {
        var response = await _httpClient.GetAsync($"/api/Auth/RefreshToken?refreshToken={refreshToken}");
        await response.EnsureSuccessStatusCodeWithApiError();
        var result = await response.Content.ReadFromJsonAsync<TokenResponse>();
        return result;
    }
}
