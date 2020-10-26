using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Server.Models
{
    public partial class MoviesDB : DbContext
    {
        public MoviesDB()
        {
        }

        public MoviesDB(DbContextOptions<MoviesDB> options)
            : base(options)
        {
        }

        public virtual DbSet<Genre> Genre { get; set; }
        public virtual DbSet<Language> Language { get; set; }
        public virtual DbSet<Movie> Movie { get; set; }
        public virtual DbSet<MovieData> MovieData { get; set; }
        public virtual DbSet<Review> Review { get; set; }
        public virtual DbSet<Style> Style { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>(entity =>
            {
                entity.HasKey(e => e.IdGenre)
                    .HasName("PK__Genre__E7B67398B0F22BF9");

                entity.Property(e => e.IdGenre).ValueGeneratedNever();

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<Language>(entity =>
            {
                entity.HasKey(e => e.IdLanguage)
                    .HasName("PK__Language__1656D917787EA72D");

                entity.Property(e => e.IdLanguage).ValueGeneratedNever();

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.HasKey(e => e.IdMovie)
                    .HasName("PK__Movie__DC0DD0EDDFAD3246");

                entity.Property(e => e.IdMovie).ValueGeneratedNever();
            });

            modelBuilder.Entity<MovieData>(entity =>
            {
                entity.HasKey(e => e.IdMovieData)
                    .HasName("PK__MovieDat__F8C19D0215DFEA55");

                entity.Property(e => e.IdMovieData).ValueGeneratedNever();

                entity.Property(e => e.Name).IsUnicode(false);

                entity.Property(e => e.Year).IsUnicode(false);
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasKey(e => e.IdReview)
                    .HasName("PK__Review__BB56047DB7D5117B");

                entity.Property(e => e.IdReview).ValueGeneratedNever();
            });

            modelBuilder.Entity<Style>(entity =>
            {
                entity.HasKey(e => e.IdStyle)
                    .HasName("PK__Style__3B87D254CF1A8651");

                entity.Property(e => e.IdStyle).ValueGeneratedNever();

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("PK__User__B7C92638C8D3559E");

                entity.Property(e => e.IdUser).ValueGeneratedNever();

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.Password).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
