namespace Server.Persistence
{
    public interface IImagesDatabaseSettings
    {
        string ImagesCollectionName { get; set; }
        string DatabaseName { get; set; }
    }
}
