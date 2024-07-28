using WebMVC.Models;
using WebMVC.Util.ExceptionHandling;

namespace WebMVC.Services.Chat;

public class HttpChatService(IHttpClientFactory httpClientFactory) : IChatService
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("ApiClient");


    public async Task<ChatDetailViewModel> GetChatDetails(Guid chatId)
    {
        var response = await _httpClient.GetAsync($"/api/Chats/{chatId}");
        await response.EnsureSuccessStatusCodeWithApiError();
        var result = await response.Content.ReadFromJsonAsync<ChatDetailViewModel>();
        return result;
    }

    public async Task<List<UserChatViewModel>> GetUserChats(Guid userId)
    {
        var response = await _httpClient.GetAsync($"/api/Chats/GetByUserId/{userId}");
        await response.EnsureSuccessStatusCodeWithApiError();
        var result = await response.Content.ReadFromJsonAsync<List<UserChatViewModel>>();
        return result;
    }

    public async Task CreateGroupAsync(string groupName)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/Chats", new { name = groupName, imageIdentifier = "group.png", invitationCode = "" });
        await response.EnsureSuccessStatusCodeWithApiError();
    }

    public async Task JoinGroupAsync(string code, Guid userId)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/Chats/Join", new { chatId = Guid.Parse(code), userId });
        await response.EnsureSuccessStatusCodeWithApiError();
    }
}


