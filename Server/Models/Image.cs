using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Server.Models
{
    public class Image
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public Object ObjectImage { get; set; }

        public DTOs.Image MapToPresentationModel()
        {
            return new DTOs.Image
            {
                Id = this.Id,
                ObjectImage = this.ObjectImage,
                Url = ""
            };
            
        }
    }
}
