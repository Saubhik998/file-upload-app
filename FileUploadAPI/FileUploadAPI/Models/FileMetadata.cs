using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace FileUploadAPI.Models
{
    public class FileMetadata
    {
        [BsonId]
        public ObjectId Id { get; set; }  // Ensure it's ObjectId, not string

        public string Filename { get; set; }
        public DateTime UploadDate { get; set; }
    }
}
