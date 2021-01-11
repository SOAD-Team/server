using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Migrations
{
    public partial class UpdateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    IdGenre = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(unicode: false, maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Genre__E7B67398A33A5261", x => x.IdGenre);
                });

            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    IdLanguage = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(unicode: false, maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Language__1656D917D9E6BCEC", x => x.IdLanguage);
                });

            migrationBuilder.CreateTable(
                name: "Style",
                columns: table => new
                {
                    IdStyle = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(unicode: false, maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Style__3B87D25469B07980", x => x.IdStyle);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    IdUser = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(unicode: false, maxLength: 89, nullable: false),
                    Password = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
                    LastName = table.Column<string>(unicode: false, maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__User__B7C92638700CD593", x => x.IdUser);
                });

            migrationBuilder.CreateTable(
                name: "Movie",
                columns: table => new
                {
                    IdMovie = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUser = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Movie__DC0DD0ED8789E0BB", x => x.IdMovie);
                    table.ForeignKey(
                        name: "FK__Movie__IdUser__603D47BB",
                        column: x => x.IdUser,
                        principalTable: "User",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MovieData",
                columns: table => new
                {
                    IdMovieData = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdMovie = table.Column<int>(nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Title = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Year = table.Column<int>(nullable: false),
                    PlatFav = table.Column<bool>(nullable: false),
                    ImageMongoId = table.Column<string>(type: "text", nullable: false),
                    IdStyle = table.Column<int>(nullable: false),
                    MetaScore = table.Column<byte>(nullable: true),
                    IMDB = table.Column<byte>(nullable: true),
                    Director = table.Column<string>(unicode: false, maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MovieDat__F8C19D024604FF2B", x => x.IdMovieData);
                    table.ForeignKey(
                        name: "FK__MovieData__IdMov__65F62111",
                        column: x => x.IdMovie,
                        principalTable: "Movie",
                        principalColumn: "IdMovie",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__MovieData__IdSty__66EA454A",
                        column: x => x.IdStyle,
                        principalTable: "Style",
                        principalColumn: "IdStyle",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    IdReview = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdMovie = table.Column<int>(nullable: false),
                    Score = table.Column<byte>(nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Review__BB56047DA5C44F2B", x => x.IdReview);
                    table.ForeignKey(
                        name: "FK__Review__IdMovie__6319B466",
                        column: x => x.IdMovie,
                        principalTable: "Movie",
                        principalColumn: "IdMovie",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MovieDataGenre",
                columns: table => new
                {
                    IdMovieDataGenre = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdMovieData = table.Column<int>(nullable: false),
                    IdGenre = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tmp_ms_x__A19A4473AD79554F", x => x.IdMovieDataGenre);
                    table.ForeignKey(
                        name: "FK__MovieData__IdGen__7BE56230",
                        column: x => x.IdGenre,
                        principalTable: "Genre",
                        principalColumn: "IdGenre",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__MovieData__IdMov__7AF13DF7",
                        column: x => x.IdMovieData,
                        principalTable: "MovieData",
                        principalColumn: "IdMovieData",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MovieDataLanguage",
                columns: table => new
                {
                    IdMovieDataLanguage = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdMovieData = table.Column<int>(nullable: false),
                    IdLanguage = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tmp_ms_x__6386009C43EC3D5E", x => x.IdMovieDataLanguage);
                    table.ForeignKey(
                        name: "FK__MovieData__IdLan__7FB5F314",
                        column: x => x.IdLanguage,
                        principalTable: "Language",
                        principalColumn: "IdLanguage",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__MovieData__IdMov__7EC1CEDB",
                        column: x => x.IdMovieData,
                        principalTable: "MovieData",
                        principalColumn: "IdMovieData",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movie_IdUser",
                table: "Movie",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_MovieData_IdMovie",
                table: "MovieData",
                column: "IdMovie");

            migrationBuilder.CreateIndex(
                name: "IX_MovieData_IdStyle",
                table: "MovieData",
                column: "IdStyle");

            migrationBuilder.CreateIndex(
                name: "IX_MovieDataGenre_IdGenre",
                table: "MovieDataGenre",
                column: "IdGenre");

            migrationBuilder.CreateIndex(
                name: "IX_MovieDataGenre_IdMovieData",
                table: "MovieDataGenre",
                column: "IdMovieData");

            migrationBuilder.CreateIndex(
                name: "IX_MovieDataLanguage_IdLanguage",
                table: "MovieDataLanguage",
                column: "IdLanguage");

            migrationBuilder.CreateIndex(
                name: "IX_MovieDataLanguage_IdMovieData",
                table: "MovieDataLanguage",
                column: "IdMovieData");

            migrationBuilder.CreateIndex(
                name: "IX_Review_IdMovie",
                table: "Review",
                column: "IdMovie");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieDataGenre");

            migrationBuilder.DropTable(
                name: "MovieDataLanguage");

            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "Genre");

            migrationBuilder.DropTable(
                name: "Language");

            migrationBuilder.DropTable(
                name: "MovieData");

            migrationBuilder.DropTable(
                name: "Movie");

            migrationBuilder.DropTable(
                name: "Style");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
