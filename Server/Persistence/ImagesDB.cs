using System;
using Server.Models;
using MongoDB.Driver;
using System.Collections.Generic;

namespace Server.Persistence
{
    public class ImagesDB : IImagesDB
    {
        private readonly IMongoCollection<Image> _Images;

        public ImagesDB(IImagesDatabaseSettings settings)
        {
            string connectionString = Environment.GetEnvironmentVariable("MONGODB_CONNECTION_STRING");
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _Images = database.GetCollection<Image>(settings.ImagesCollectionName);
        }

        public List<Image> Get() =>
            _Images.Find(Image => true).ToList();

        public Image Get(string id) =>
            _Images.Find<Image>(Image => Image.Id == id).FirstOrDefault();

        public Image Create(Image Image)
        {
            _Images.InsertOne(Image);
            return Image;
        }

        public void Update(string id, Image ImageIn) =>
            _Images.ReplaceOne(Image => Image.Id == id, ImageIn);

        public void Remove(Image ImageIn) =>
            _Images.DeleteOne(Image => Image.Id == ImageIn.Id);

        public void Remove(string id) =>
            _Images.DeleteOne(Image => Image.Id == id);
    }
}
