using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.File;


public interface IFileService
{
    Task<Guid> UploadFileAsync(IFormFile file);
    Task<Stream> GetFileAsync(Guid id);
    Task DeleteFileAsync(Guid id);
}
