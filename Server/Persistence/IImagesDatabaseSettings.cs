namespace Server.Models
{
    public interface IImagesDatabaseSettings
    {
        string ImagesCollectionName { get; set; }
        string DatabaseName { get; set; }
    }
}
