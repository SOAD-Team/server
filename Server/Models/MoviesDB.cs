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
                    .HasName("PK__Genre__E7B6739867A54C3B");

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<Language>(entity =>
            {
                entity.HasKey(e => e.IdLanguage)
                    .HasName("PK__Language__1656D917D6CB949A");

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.HasKey(e => e.IdMovie)
                    .HasName("PK__Movie__DC0DD0ED70FF573B");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Movie)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Movie__IdUser__4959E263");
            });

            modelBuilder.Entity<MovieData>(entity =>
            {
                entity.HasKey(e => e.IdMovieData)
                    .HasName("PK__MovieDat__F8C19D0289491157");

                entity.Property(e => e.Title).IsUnicode(false);

                entity.Property(e => e.Year).IsUnicode(false);

                entity.HasOne(d => d.IdMovieNavigation)
                    .WithMany(p => p.MovieData)
                    .HasForeignKey(d => d.IdMovie)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MovieData__IdMov__4F12BBB9");

                entity.HasOne(d => d.IdStyleNavigation)
                    .WithMany(p => p.MovieData)
                    .HasForeignKey(d => d.IdStyle)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MovieData__IdSty__5006DFF2");
            });

            modelBuilder.Entity<MovieDataGenre>(entity =>
            {
                entity.HasNoKey();

                entity.HasOne(d => d.IdGenreNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdGenre)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MovieData__IdGen__55BFB948");

                entity.HasOne(d => d.IdMovieDataNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdMovieData)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MovieData__IdMov__54CB950F");
            });

            modelBuilder.Entity<MovieDataLanguage>(entity =>
            {
                entity.HasNoKey();

                entity.HasOne(d => d.IdLanguageNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdLanguage)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MovieData__IdLan__52E34C9D");

                entity.HasOne(d => d.IdMovieDataNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdMovieData)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MovieData__IdMov__51EF2864");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasKey(e => e.IdReview)
                    .HasName("PK__Review__BB56047DB55783BD");

                entity.HasOne(d => d.IdMovieNavigation)
                    .WithMany(p => p.Review)
                    .HasForeignKey(d => d.IdMovie)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Review__IdMovie__4C364F0E");
            });

            modelBuilder.Entity<Style>(entity =>
            {
                entity.HasKey(e => e.IdStyle)
                    .HasName("PK__Style__3B87D254CE3AF50D");

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("PK__User__B7C926386878CCAD");

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
