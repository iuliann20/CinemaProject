using Microsoft.EntityFrameworkCore.Migrations;

namespace CinemaProject.DAL.Migrations
{
   public partial class IncreaseMovieNameLength : Migration
   {
      protected override void Up(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.AlterColumn<string>(
             name: "MovieName",
             table: "CinemaMovies",
             type: "nvarchar(100)",
             maxLength: 100,
             nullable: true,
             oldClrType: typeof(string),
             oldType: "nvarchar(25)",
             oldMaxLength: 25,
             oldNullable: true);
      }

      protected override void Down(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.AlterColumn<string>(
             name: "MovieName",
             table: "CinemaMovies",
             type: "nvarchar(25)",
             maxLength: 25,
             nullable: true,
             oldClrType: typeof(string),
             oldType: "nvarchar(100)",
             oldMaxLength: 100,
             oldNullable: true);
      }
   }
}
