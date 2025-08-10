using FileAPI.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace FileAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ImageController : ControllerBase
{
    private string[] allowedExtensions = [".jpg", ".jpeg", ".png", ".gif"];

    [HttpPost("upload")]
    public async Task<IActionResult> UploadAsync([FromForm]FileUploadRequest request)
    {
        var file = request.File;

        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded.");

        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

        if(!allowedExtensions.Contains(extension))
            return BadRequest("Invalid file type. Allowed types are: " + string.Join(", ", allowedExtensions));

        var imageFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
        if (!Directory.Exists(imageFolder))
            Directory.CreateDirectory(imageFolder);


        var fileName = Guid.NewGuid() + extension;
        var filePath = Path.Combine(imageFolder, fileName);

        using var stream = new FileStream(filePath, FileMode.Create);

        await file.CopyToAsync(stream);

        var imageName = fileName;
        var imagePath = Path.Combine("images", fileName);
        var imageUrl = $"{Request.Scheme}://{Request.Host}/images/{fileName}";

        var response = new FileUploadResponse(imageName, imagePath, imageUrl);

        return Ok(response);
    }

    [HttpDelete("delete")]
    public IActionResult Delete([FromBody] FileDeleteRequest request)
    {
        var imageFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

        var imagePath = Path.Combine(imageFolder, request.FileName);

        if (System.IO.File.Exists(imagePath))
        {
            System.IO.File.Delete(imagePath);
        }
        return Ok();
    }
}
