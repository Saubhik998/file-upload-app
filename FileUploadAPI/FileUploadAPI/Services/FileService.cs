using FileUploadAPI.Models;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileUploadAPI.Services
{
    public class FileService : IFileService
    {
        private readonly GridFSBucket _gridFSBucket;
        private readonly IMongoCollection<FileMetadata> _metadataCollection;

        public FileService(IMongoClient mongoClient, bool isTestEnvironment = false)
        {
            var databaseName = isTestEnvironment ? "FileUploadAPI_TestDB" : "FileUploadDB";
            Console.WriteLine($"Using Database: {databaseName}");

            var database = mongoClient.GetDatabase(databaseName);
            _gridFSBucket = new GridFSBucket(database);
            _metadataCollection = database.GetCollection<FileMetadata>("FileMetadata");
        }

        public async Task<IEnumerable<FileMetadata>> GetAllFilesAsync()
        {
            return await _metadataCollection.Find(_ => true).ToListAsync();
        }

        public async Task<FileMetadata> GetFileByIdAsync(string id)
        {
            try
            {
                var objectId = new ObjectId(id); // Convert string to ObjectId
                var file = await _metadataCollection.Find(f => f.Id == objectId).FirstOrDefaultAsync();
                return file;
            }
            catch (FormatException)
            {
                return null; // Return null if the ID is not valid
            }
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            using var stream = file.OpenReadStream();
            var fileId = await _gridFSBucket.UploadFromStreamAsync(file.FileName, stream);

            var metadata = new FileMetadata
            {
                Id = fileId,  // Store ObjectId from GridFS
                Filename = file.FileName,
                UploadDate = DateTime.UtcNow
            };

            await _metadataCollection.InsertOneAsync(metadata);

            return fileId.ToString(); // Return the ObjectId as string for easy reference
        }

        public async Task<bool> DeleteFileAsync(string id)
        {
            try
            {
                var objectId = new ObjectId(id); // Convert string to ObjectId

                // Delete the file from GridFS
                await _gridFSBucket.DeleteAsync(objectId);

                // Delete the file metadata from the collection
                var deleteResult = await _metadataCollection.DeleteOneAsync(f => f.Id == objectId);
                return deleteResult.DeletedCount > 0;
            }
            catch (FormatException)
            {
                return false; // Return false if the ID is invalid
            }
            catch
            {
                return false; // Return false for other errors
            }
        }
    }
}
