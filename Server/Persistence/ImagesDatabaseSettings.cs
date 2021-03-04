namespace Server.Persistence
{
    public class ImagesDatabaseSettings : IImagesDatabaseSettings
    {
        public string ImagesCollectionName { get; set; }
        public string DatabaseName { get; set; }
    }
}
