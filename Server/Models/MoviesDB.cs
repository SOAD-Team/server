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
                    .HasName("PK__Genre__E7B67398E1392B01");

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<Language>(entity =>
            {
                entity.HasKey(e => e.IdLanguage)
                    .HasName("PK__Language__1656D9172A948841");

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.HasKey(e => e.IdMovie)
                    .HasName("PK__Movie__DC0DD0EDDD951EF4");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Movie)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Movie__IdUser__7D0E9093");
            });

            modelBuilder.Entity<MovieData>(entity =>
            {
                entity.HasKey(e => e.IdMovieData)
                    .HasName("PK__MovieDat__F8C19D02681336E7");

                entity.Property(e => e.Title).IsUnicode(false);

                entity.Property(e => e.Year).IsUnicode(false);

                entity.HasOne(d => d.IdMovieNavigation)
                    .WithMany(p => p.MovieData)
                    .HasForeignKey(d => d.IdMovie)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MovieData__IdMov__02C769E9");

                entity.HasOne(d => d.IdStyleNavigation)
                    .WithMany(p => p.MovieData)
                    .HasForeignKey(d => d.IdStyle)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MovieData__IdSty__03BB8E22");
            });

            modelBuilder.Entity<MovieDataGenre>(entity =>
            {
                entity.HasNoKey();

                entity.HasOne(d => d.IdGenreNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdGenre)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MovieData__IdGen__09746778");

                entity.HasOne(d => d.IdMovieDataNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdMovieData)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MovieData__IdMov__0880433F");
            });

            modelBuilder.Entity<MovieDataLanguage>(entity =>
            {
                entity.HasNoKey();

                entity.HasOne(d => d.IdLanguageNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdLanguage)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MovieData__IdLan__0697FACD");

                entity.HasOne(d => d.IdMovieDataNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdMovieData)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MovieData__IdMov__05A3D694");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasKey(e => e.IdReview)
                    .HasName("PK__Review__BB56047D2757F48F");

                entity.HasOne(d => d.IdMovieNavigation)
                    .WithMany(p => p.Review)
                    .HasForeignKey(d => d.IdMovie)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Review__IdMovie__7FEAFD3E");
            });

            modelBuilder.Entity<Style>(entity =>
            {
                entity.HasKey(e => e.IdStyle)
                    .HasName("PK__Style__3B87D2547B5CECA2");

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("PK__User__B7C92638FB0CCBD9");

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.Password).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
