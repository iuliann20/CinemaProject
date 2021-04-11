using Microsoft.EntityFrameworkCore.Migrations;

namespace CinemaProject.DAL.Migrations
{
   public partial class AddMoviePhotoToMovieTable : Migration
   {
      protected override void Up(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.AddColumn<string>(
             name: "MoviePhoto",
             table: "CinemaMovies",
             type: "nvarchar(max)",
             nullable: true);
      }

      protected override void Down(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.DropColumn(
             name: "MoviePhoto",
             table: "CinemaMovies");
      }
   }
}
