using Application.DTOs.FileApi;
using Application.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Adapters;

public class FileApiService : IFileService
{
    private readonly HttpClient client;

    public FileApiService(IHttpClientFactory clientFactory)
    {
        client = clientFactory.CreateClient("FileApi");
    }

    public async Task DeleteFileAsync(string fileName)
    {
        var body = new FileDeleteRequest { FileName = fileName };

        var request = new HttpRequestMessage(HttpMethod.Delete, "api/image/delete")
        {
            Content = JsonContent.Create(body)
        };

        var response = await client.SendAsync(request);
    }

    public async Task<FileUploadResponse> UploadFileAsync(IFormFile file)
    {
        var content = new MultipartFormDataContent();

        var streamContent = new StreamContent(file.OpenReadStream())
        {
            Headers =
                {
                    ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType)
                }
        };

        content.Add(streamContent, "File", file.FileName);

        var response = await client.PostAsync("api/image/upload", content);

        var result = await response.Content.ReadFromJsonAsync<FileUploadResponse>();
        return result;
    }
}
