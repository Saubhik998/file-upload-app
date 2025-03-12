using FileUploadAPI.Controllers;
using FileUploadAPI.Models;
using FileUploadAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Text;
using Xunit;

namespace FileUploadAPI.Tests
{
    public class FilesControllerTests
    {
        private readonly FilesController _controller;
        private readonly IFileService _fileService;

        public FilesControllerTests()
        {
            var mongoClient = new MongoClient("mongodb://localhost:27017");
            _fileService = new FileService(mongoClient, true); // Setting true to use the test database
            _controller = new FilesController(_fileService);
        }

        [Fact]
        public async Task UploadFile_ReturnsOkResult()
        {
            var fileMock = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("Fake file content")), 0, 20, "Data", "test.txt");

            var result = await _controller.UploadFile(fileMock);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetFileById_ReturnsOkResult()
        {
            var result = await _controller.GetFileById("YOUR_TEST_FILE_ID");
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetAllFiles_ReturnsOkResult()
        {
            var result = await _controller.GetAllFiles();
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteFile_ReturnsOkResult()
        {
            var result = await _controller.DeleteFile("YOUR_TEST_FILE_ID");
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
