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
                    .HasName("PK__Genre__E7B673981C02E624");

                entity.Property(e => e.IdGenre).ValueGeneratedNever();

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<Language>(entity =>
            {
                entity.HasKey(e => e.IdLanguage)
                    .HasName("PK__Language__1656D9174576F698");

                entity.Property(e => e.IdLanguage).ValueGeneratedNever();

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.HasKey(e => e.IdMovie)
                    .HasName("PK__Movie__DC0DD0EDC5291B5A");

                entity.Property(e => e.IdMovie).ValueGeneratedNever();

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Movie)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("FK__Movie__IdUser__5F7E2DAC");
            });

            modelBuilder.Entity<MovieData>(entity =>
            {
                entity.HasKey(e => e.IdMovieData)
                    .HasName("PK__MovieDat__F8C19D024B051A4D");

                entity.Property(e => e.IdMovieData).ValueGeneratedNever();

                entity.Property(e => e.Name).IsUnicode(false);

                entity.Property(e => e.Year).IsUnicode(false);

                entity.HasOne(d => d.IdMovieNavigation)
                    .WithMany(p => p.MovieData)
                    .HasForeignKey(d => d.IdMovie)
                    .HasConstraintName("FK__MovieData__IdMov__65370702");

                entity.HasOne(d => d.IdStyleNavigation)
                    .WithMany(p => p.MovieData)
                    .HasForeignKey(d => d.IdStyle)
                    .HasConstraintName("FK__MovieData__IdSty__662B2B3B");
            });

            modelBuilder.Entity<MovieDataGenre>(entity =>
            {
                entity.HasNoKey();

                entity.HasOne(d => d.IdGenreNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdGenre)
                    .HasConstraintName("FK__MovieData__IdGen__6BE40491");

                entity.HasOne(d => d.IdMovieDataNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdMovieData)
                    .HasConstraintName("FK__MovieData__IdMov__6AEFE058");
            });

            modelBuilder.Entity<MovieDataLanguage>(entity =>
            {
                entity.HasNoKey();

                entity.HasOne(d => d.IdLanguageNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdLanguage)
                    .HasConstraintName("FK__MovieData__IdLan__690797E6");

                entity.HasOne(d => d.IdMovieDataNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdMovieData)
                    .HasConstraintName("FK__MovieData__IdMov__681373AD");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasKey(e => e.IdReview)
                    .HasName("PK__Review__BB56047D494F0583");

                entity.Property(e => e.IdReview).ValueGeneratedNever();

                entity.HasOne(d => d.IdMovieNavigation)
                    .WithMany(p => p.Review)
                    .HasForeignKey(d => d.IdMovie)
                    .HasConstraintName("FK__Review__IdMovie__625A9A57");
            });

            modelBuilder.Entity<Style>(entity =>
            {
                entity.HasKey(e => e.IdStyle)
                    .HasName("PK__Style__3B87D2549FE5FA5A");

                entity.Property(e => e.IdStyle).ValueGeneratedNever();

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("PK__User__B7C9263830B55887");

                entity.Property(e => e.IdUser).ValueGeneratedNever();

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.Password).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
