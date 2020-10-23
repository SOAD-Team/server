using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Migrations
{
    public partial class MoviesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movie",
                columns: table => new
                {
                    IdMovie = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Director = table.Column<string>(maxLength: 30, nullable: true),
                    Year = table.Column<DateTime>(type: "date", nullable: false),
                    Genre = table.Column<int>(nullable: false),
                    Language = table.Column<int>(nullable: false),
                    IsFavorite = table.Column<bool>(nullable: false),
                    Qualification = table.Column<byte>(nullable: false),
                    ImageId = table.Column<string>(nullable: true),
                    IMDB = table.Column<byte>(nullable: true),
                    Style = table.Column<int>(nullable: false),
                    MetaScore = table.Column<byte>(nullable: true),
                    Popurarity = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.IdMovie);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movie");
        }
    }
}
