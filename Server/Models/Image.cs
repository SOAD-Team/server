using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Server.Models
{
    public class Image
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public byte[] ObjectImage { get; set; }

        public Image(byte[] objectImage)
        {
            this.ObjectImage = objectImage;
        }

        public Image(string id, byte[] objectImage)
        {
            this.Id = id;
            this.ObjectImage = objectImage;
        }

        public DTOs.Image MapToPresentationModel()
        {
            return new DTOs.Image
            {
                Id = this.Id,
                ObjectImage = null,
                Url = ""
            };
            
        }
    }
}
