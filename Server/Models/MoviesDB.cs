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
            string connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
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