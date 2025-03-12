using FileUploadAPI.Models;
using FileUploadAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FileUploadAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IFileService _fileService;

        public FilesController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            var fileId = await _fileService.UploadFileAsync(file);
            return Ok(new { Message = "File uploaded successfully", FileId = fileId });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFileById(string id)
        {
            var file = await _fileService.GetFileByIdAsync(id);
            return Ok(file);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFiles()
        {
            var files = await _fileService.GetAllFilesAsync();
            return Ok(files);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFile(string id)
        {
            var result = await _fileService.DeleteFileAsync(id);
            if (result)
                return Ok(new { Message = "File deleted successfully" });

            return NotFound();
        }
    }
}
