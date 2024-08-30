using EventWeb.Application.Abstractions;
using Microsoft.AspNetCore.Http;

namespace EventWeb.Infrastructure; 

public class FileService : IFileService
{
    private readonly string[] _permittedExtensions = [".jpg", ".jpeg", ".png", ".gif"];


    public async Task<string> SaveFileAsync(IFormFile file, string storagePath)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("Invalid file");

        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

        if (!_permittedExtensions.Contains(extension))
            throw new InvalidOperationException("Unsupported file extension");

        var uniqueFileName = $"{Path.GetFileNameWithoutExtension(file.FileName)}_{Guid.NewGuid()}{extension}";

        var filePath = Path.Combine(storagePath, uniqueFileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return filePath;
    }

    public async Task<byte[]> LoadFileAsync(string? filePath)
    {

        if (!File.Exists(filePath))
            throw new FileNotFoundException("File not found", filePath);

        return await File.ReadAllBytesAsync(filePath);
    }

    public void DeleteFile(string? filePath)
    {

        if (File.Exists(filePath) && filePath!=null)
        {
            File.Delete(filePath);
        }
        else
        {
            throw new FileNotFoundException("File not found", filePath);
        }
    }
}