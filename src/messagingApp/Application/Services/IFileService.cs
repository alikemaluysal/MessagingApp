using Application.DTOs.FileApi;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services;

public interface IFileService
{
    Task<FileUploadResponse> UploadFileAsync(IFormFile file);
    Task DeleteFileAsync(string fileName);
}
