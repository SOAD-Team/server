namespace Server.Resources
{
    public class KeyValuePair
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public static KeyValuePair Empty { get => new KeyValuePair { Id = 0, Name = "" }; }
    }
}
