using WebMVC.Models;
using WebMVC.Util.ExceptionHandling;

namespace WebMVC.Services.Message;

public class HttpMessageService(IHttpClientFactory httpClientFactory) : IMessageService
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("ApiClient");

    public async Task<List<ChatMessageViewModel>> GetChatMessagesAsync(Guid chatId)
    {
        var response = await _httpClient.GetAsync($"/api/Messages/GetByChatId/{chatId}");
        await response.EnsureSuccessStatusCodeWithApiError();
        var result = await response.Content.ReadFromJsonAsync<List<ChatMessageViewModel>>();
        return result;
    }

    public async Task<ChatMessageViewModel> SendMessageAsync(ChatMessageViewModel message)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/Messages/SendMessage", message);
        await response.EnsureSuccessStatusCodeWithApiError();
        var result = await response.Content.ReadFromJsonAsync<ChatMessageViewModel>();
        return result;
    }
}
