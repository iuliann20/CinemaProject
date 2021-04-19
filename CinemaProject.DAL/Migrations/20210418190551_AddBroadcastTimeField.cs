using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace CinemaProject.DAL.Migrations
{
   public partial class AddBroadcastTimeField : Migration
   {
      protected override void Up(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.AddColumn<DateTime>(
             name: "BroadcastTime",
             table: "CinemaBroadcasts",
             type: "datetime2",
             nullable: false,
             defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
      }

      protected override void Down(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.DropColumn(
             name: "BroadcastTime",
             table: "CinemaBroadcasts");
      }
   }
}
