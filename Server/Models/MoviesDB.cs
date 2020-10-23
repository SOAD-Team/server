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
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
            optionsBuilder.UseSqlServer(connectionString);
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>(
                entity => {
                    entity.HasKey(e => e.IdMovie).HasName("PRIMARY");
                    entity.Property(e => e.Name).IsUnicode();
                    entity.Property(e => e.Director).IsUnicode();
                    entity.Property(e => e.Genre).HasConversion<int>();
                    entity.Property(e => e.Language).HasConversion<int>();
                    entity.Property(e => e.IsFavorite);
                    entity.Property(e => e.Qualification);
                    entity.Property(e => e.ImageId).IsUnicode();
                    entity.Property(e => e.IMDB);
                    entity.Property(e => e.Style).HasConversion<int>();
                    entity.Property(e => e.Popularity);
                }
            );
        }
    }
}