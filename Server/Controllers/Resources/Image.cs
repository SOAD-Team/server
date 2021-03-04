namespace Server.Resources
{
    public class Image
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public static Image Empty { get => new Image { Id = "", Url = "" }; }
    }
}
