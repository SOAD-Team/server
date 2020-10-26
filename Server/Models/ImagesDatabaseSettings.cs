using System;
namespace Server.Models
{
    public interface IImagesDatabaseSettings
    {
        string ImagesCollectionName { get; set; }
        string DatabaseName { get; set; }
    }
    public class ImagesDatabaseSettings : IImagesDatabaseSettings
    {
        public string ImagesCollectionName { get; set; }
        public string DatabaseName { get; set; }
    }
}
