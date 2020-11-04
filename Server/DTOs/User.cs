namespace Server.DTOs
{
    public class User
    {
        public static User Empty { get => empty(); }

        private static User empty()
        {
            User data = new User();
            data.IdUser = 0;
            data.Email = "";
            data.IsActive = true;
            data.LastName = "";
            data.Email = "";
            data.Name = "";
            data.Password = "";

            return data;
        }

        

        public int IdUser { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; } = true;
        public string Name { get; set; }
        public string LastName { get; set; }

    }
}
