using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    public partial class Language
    {
        public Language()
        {
            MovieDataLanguage = new HashSet<MovieDataLanguage>();
        }
        public Language(string name)
        {
            MovieDataLanguage = new HashSet<MovieDataLanguage>();
            Name = name;
        }
        public Language(int idLanguage, string name)
        {
            MovieDataLanguage = new HashSet<MovieDataLanguage>();
            IdLanguage = idLanguage;
            Name = name;
        }
        [Key]
        public int IdLanguage { get; set; }
        [Required]
        [StringLength(15)]
        public string Name { get; set; }

        [InverseProperty("IdLanguageNavigation")]
        public virtual ICollection<MovieDataLanguage> MovieDataLanguage { get; set; }

        public static Language Empty { get => empty(); }

        private static Language empty()
        {
            Language value = new Language();
            const string name = "";
            value.Name = name;
            return value;
        }
    }
}
