using Microsoft.AspNetCore.Http;

namespace EventWeb.Application.Abstractions
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(IFormFile file, string storagePath);

        Task<byte[]> LoadFileAsync(string? filePath);

        void DeleteFile(string? filePath);
    }
}