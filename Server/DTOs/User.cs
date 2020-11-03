using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.DTOs
{
    public class User
    {
        public static User Empty { get; set; }
        public int IdUser { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; } = true;
        public string Name { get; set; }
        public string LastName { get; set; }

    }
}
