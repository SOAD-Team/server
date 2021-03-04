using System;
using Server.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<List<Image>> Get() =>
            await _Images.Find(Image => true).ToListAsync();

        public async Task<Image> Get(string id) =>
            await _Images.Find<Image>(Image => Image.Id == id).FirstOrDefaultAsync();

        public async Task<Image> Create(Image Image)
        {
           await _Images.InsertOneAsync(Image);
            return Image;
        }

        public async void Update(string id, Image ImageIn) =>
            await _Images.ReplaceOneAsync(Image => Image.Id == id, ImageIn);

        public async void Remove(Image ImageIn) =>
            await _Images.DeleteOneAsync(Image => Image.Id == ImageIn.Id);

        public async void Remove(string id) =>
            await _Images.DeleteOneAsync(Image => Image.Id == id);
    }
}
