using Application.Services.File;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Services.File;

public class FileApiAdapter(HttpClient httpClient, IConfiguration configuration) : IFileService
{
    private readonly string fileApiUrl = configuration["FileApiUrl"] ?? 
        throw new InvalidOperationException("FileApiUrl can not be found in configuration");
   
    //TODO: Implement DeleteFileAsync (also in api)
    public Task DeleteFileAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Stream> GetFileAsync(Guid id)
    {
        var response = await httpClient.GetAsync($"{fileApiUrl}/api/File/Download?id={id}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStreamAsync();
    }

    public async Task<Guid> UploadFileAsync(IFormFile file)
    {
        using var content = new MultipartFormDataContent();
        using var fileSteram = file.OpenReadStream();
        var streamContent = new StreamContent(fileSteram);

        streamContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
        {
            Name = "file",
            FileName = file.FileName
        };
        content.Add(streamContent);
        var response = await httpClient.PostAsync($"{fileApiUrl}/api/File/Upload", content);

        response.EnsureSuccessStatusCode();

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<FileUploadResponse>(jsonResponse, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return result!.Id;

    }
}
