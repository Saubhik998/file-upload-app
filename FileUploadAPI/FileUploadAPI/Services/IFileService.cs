using FileUploadAPI.Models;
using Microsoft.AspNetCore.Http;

namespace FileUploadAPI.Services
{
    public interface IFileService
    {
        Task<IEnumerable<FileMetadata>> GetAllFilesAsync();
        Task<FileMetadata> GetFileByIdAsync(string id);
        Task<string> UploadFileAsync(IFormFile file);
        Task<bool> DeleteFileAsync(string id);
    }
}
