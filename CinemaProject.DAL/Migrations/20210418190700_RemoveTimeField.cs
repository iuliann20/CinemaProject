using Microsoft.EntityFrameworkCore.Migrations;

namespace CinemaProject.DAL.Migrations
{
   public partial class RemoveTimeField : Migration
   {
      protected override void Up(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.DropColumn(
             name: "Time",
             table: "CinemaBroadcasts");
      }

      protected override void Down(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.AddColumn<int>(
             name: "Time",
             table: "CinemaBroadcasts",
             type: "int",
             nullable: false,
             defaultValue: 0);
      }
   }
}
