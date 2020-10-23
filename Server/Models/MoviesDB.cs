using Microsoft.EntityFrameworkCore;
using System;

namespace Server.Models
{
    public class MoviesDB : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public MoviesDB() : base()
        {

        }
        public MoviesDB(DbContextOptions<MoviesDB> options) : base(options)
        {
            this.LoadExampleData();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Server=tcp:my-movie.database.windows.net,1433;Initial Catalog=my-movie-database;Persist Security Info=False;User ID=Abstractize;Password=Leirbag36011999;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            optionsBuilder.UseSqlServer(connectionString);
        }

        private void LoadExampleData()
        {
            //Metan un API de Pelis que Cargue los datos.
            Movies.Add(new Movie("La Super Pelicula", "Yo", new DateTime(), Enums.Genre.Horror, Enums.Language.English, false, 0, "Una Imagen GG", null, Enums.Style.SomeStyle, null, 89
            ));
        }
    }
}