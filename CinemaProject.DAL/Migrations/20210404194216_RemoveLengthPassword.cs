using Microsoft.EntityFrameworkCore.Migrations;

namespace CinemaProject.DAL.Migrations
{
   public partial class RemoveLengthPassword : Migration
   {
      protected override void Up(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.AlterColumn<string>(
             name: "Password",
             table: "Users",
             type: "nvarchar(max)",
             nullable: true,
             oldClrType: typeof(string),
             oldType: "nvarchar(25)",
             oldMaxLength: 25,
             oldNullable: true);
      }

      protected override void Down(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.AlterColumn<string>(
             name: "Password",
             table: "Users",
             type: "nvarchar(25)",
             maxLength: 25,
             nullable: true,
             oldClrType: typeof(string),
             oldType: "nvarchar(max)",
             oldNullable: true);
      }
   }
}
