namespace Server.Resources
{
    public class Review
    {
        public int IdReview { get; set; }
        public int IdMovie { get; set; }
        public byte Score { get; set; }
        public string Comment { get; set; }

        public static Review Empty { get => new Review(); }
    }
}
