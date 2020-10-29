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
        public virtual DbSet<MovieDataGenre> MovieDataGenre { get; set; }
        public virtual DbSet<MovieDataLanguage> MovieDataLanguage { get; set; }
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
                    .HasName("PK__Genre__E7B67398A33A5261");

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<Language>(entity =>
            {
                entity.HasKey(e => e.IdLanguage)
                    .HasName("PK__Language__1656D917D9E6BCEC");

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.HasKey(e => e.IdMovie)
                    .HasName("PK__Movie__DC0DD0ED8789E0BB");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Movie)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Movie__IdUser__603D47BB");
            });

            modelBuilder.Entity<MovieData>(entity =>
            {
                entity.HasKey(e => e.IdMovieData)
                    .HasName("PK__MovieDat__F8C19D024604FF2B");

                entity.Property(e => e.Title).IsUnicode(false);

                entity.HasOne(d => d.IdMovieNavigation)
                    .WithMany(p => p.MovieData)
                    .HasForeignKey(d => d.IdMovie)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MovieData__IdMov__65F62111");

                entity.HasOne(d => d.IdStyleNavigation)
                    .WithMany(p => p.MovieData)
                    .HasForeignKey(d => d.IdStyle)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MovieData__IdSty__66EA454A");
            });

            modelBuilder.Entity<MovieDataGenre>(entity =>
            {
                entity.HasNoKey();

                entity.HasOne(d => d.IdGenreNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdGenre)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MovieData__IdGen__6CA31EA0");

                entity.HasOne(d => d.IdMovieDataNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdMovieData)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MovieData__IdMov__6BAEFA67");
            });

            modelBuilder.Entity<MovieDataLanguage>(entity =>
            {
                entity.HasNoKey();

                entity.HasOne(d => d.IdLanguageNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdLanguage)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MovieData__IdLan__69C6B1F5");

                entity.HasOne(d => d.IdMovieDataNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdMovieData)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MovieData__IdMov__68D28DBC");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasKey(e => e.IdReview)
                    .HasName("PK__Review__BB56047DA5C44F2B");

                entity.HasOne(d => d.IdMovieNavigation)
                    .WithMany(p => p.Review)
                    .HasForeignKey(d => d.IdMovie)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Review__IdMovie__6319B466");
            });

            modelBuilder.Entity<Style>(entity =>
            {
                entity.HasKey(e => e.IdStyle)
                    .HasName("PK__Style__3B87D25469B07980");

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("PK__User__B7C92638700CD593");

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.LastName).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);

                entity.Property(e => e.Password).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
