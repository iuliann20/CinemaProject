using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace CinemaProject.DAL.Migrations
{
   public partial class InitialMigration : Migration
   {
      protected override void Up(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.CreateTable(
             name: "Actors",
             columns: table => new {
                ActorId = table.Column<int>(type: "int", nullable: false)
                     .Annotation("SqlServer:Identity", "1, 1"),
                ActorName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true)
             },
             constraints: table => {
                table.PrimaryKey("PK_Actors", x => x.ActorId);
             });

         migrationBuilder.CreateTable(
             name: "CinemaLocations",
             columns: table => new {
                LocationId = table.Column<int>(type: "int", nullable: false)
                     .Annotation("SqlServer:Identity", "1, 1"),
                NameLocation = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                AddressLocation = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
             },
             constraints: table => {
                table.PrimaryKey("PK_CinemaLocations", x => x.LocationId);
             });

         migrationBuilder.CreateTable(
             name: "CinemaMovies",
             columns: table => new {
                MovieId = table.Column<int>(type: "int", nullable: false)
                     .Annotation("SqlServer:Identity", "1, 1"),
                MovieName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Duration = table.Column<int>(type: "int", nullable: false)
             },
             constraints: table => {
                table.PrimaryKey("PK_CinemaMovies", x => x.MovieId);
             });

         migrationBuilder.CreateTable(
             name: "CinemaPrices",
             columns: table => new {
                PriceId = table.Column<int>(type: "int", nullable: false)
                     .Annotation("SqlServer:Identity", "1, 1"),
                Price = table.Column<int>(type: "int", nullable: false)
             },
             constraints: table => {
                table.PrimaryKey("PK_CinemaPrices", x => x.PriceId);
             });

         migrationBuilder.CreateTable(
             name: "CinemaRoles",
             columns: table => new {
                RoleId = table.Column<int>(type: "int", nullable: false)
                     .Annotation("SqlServer:Identity", "1, 1"),
                Role = table.Column<string>(type: "nvarchar(max)", nullable: true)
             },
             constraints: table => {
                table.PrimaryKey("PK_CinemaRoles", x => x.RoleId);
             });

         migrationBuilder.CreateTable(
             name: "Users",
             columns: table => new {
                UserId = table.Column<int>(type: "int", nullable: false)
                     .Annotation("SqlServer:Identity", "1, 1"),
                FirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                LastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                BirthDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                Password = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true)
             },
             constraints: table => {
                table.PrimaryKey("PK_Users", x => x.UserId);
             });

         migrationBuilder.CreateTable(
             name: "MovieActors",
             columns: table => new {
                MovieActorId = table.Column<int>(type: "int", nullable: false)
                     .Annotation("SqlServer:Identity", "1, 1"),
                MovieId = table.Column<int>(type: "int", nullable: false),
                ActorId = table.Column<int>(type: "int", nullable: false)
             },
             constraints: table => {
                table.PrimaryKey("PK_MovieActors", x => x.MovieActorId);
                table.ForeignKey(
                       name: "FK_MovieActors_Actors_ActorId",
                       column: x => x.ActorId,
                       principalTable: "Actors",
                       principalColumn: "ActorId",
                       onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                       name: "FK_MovieActors_CinemaMovies_MovieId",
                       column: x => x.MovieId,
                       principalTable: "CinemaMovies",
                       principalColumn: "MovieId",
                       onDelete: ReferentialAction.Cascade);
             });

         migrationBuilder.CreateTable(
             name: "CinemaBroadcasts",
             columns: table => new {
                BroadcastId = table.Column<int>(type: "int", nullable: false)
                     .Annotation("SqlServer:Identity", "1, 1"),
                MovieId = table.Column<int>(type: "int", nullable: false),
                CinemaLocationId = table.Column<int>(type: "int", nullable: false),
                PriceId = table.Column<int>(type: "int", nullable: false),
                Time = table.Column<int>(type: "int", nullable: false)
             },
             constraints: table => {
                table.PrimaryKey("PK_CinemaBroadcasts", x => x.BroadcastId);
                table.ForeignKey(
                       name: "FK_CinemaBroadcasts_CinemaLocations_CinemaLocationId",
                       column: x => x.CinemaLocationId,
                       principalTable: "CinemaLocations",
                       principalColumn: "LocationId",
                       onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                       name: "FK_CinemaBroadcasts_CinemaMovies_MovieId",
                       column: x => x.MovieId,
                       principalTable: "CinemaMovies",
                       principalColumn: "MovieId",
                       onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                       name: "FK_CinemaBroadcasts_CinemaPrices_PriceId",
                       column: x => x.PriceId,
                       principalTable: "CinemaPrices",
                       principalColumn: "PriceId",
                       onDelete: ReferentialAction.Cascade);
             });

         migrationBuilder.CreateTable(
             name: "AccessTokens",
             columns: table => new {
                TokenId = table.Column<int>(type: "int", nullable: false)
                     .Annotation("SqlServer:Identity", "1, 1"),
                AccessToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                UserId = table.Column<int>(type: "int", nullable: false)
             },
             constraints: table => {
                table.PrimaryKey("PK_AccessTokens", x => x.TokenId);
                table.ForeignKey(
                       name: "FK_AccessTokens_Users_UserId",
                       column: x => x.UserId,
                       principalTable: "Users",
                       principalColumn: "UserId",
                       onDelete: ReferentialAction.Cascade);
             });

         migrationBuilder.CreateTable(
             name: "CinemaReviews",
             columns: table => new {
                ReviewId = table.Column<int>(type: "int", nullable: false)
                     .Annotation("SqlServer:Identity", "1, 1"),
                UserId = table.Column<int>(type: "int", nullable: false),
                MovieId = table.Column<int>(type: "int", nullable: false),
                Review = table.Column<string>(type: "nvarchar(max)", nullable: true)
             },
             constraints: table => {
                table.PrimaryKey("PK_CinemaReviews", x => x.ReviewId);
                table.ForeignKey(
                       name: "FK_CinemaReviews_CinemaMovies_MovieId",
                       column: x => x.MovieId,
                       principalTable: "CinemaMovies",
                       principalColumn: "MovieId",
                       onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                       name: "FK_CinemaReviews_Users_UserId",
                       column: x => x.UserId,
                       principalTable: "Users",
                       principalColumn: "UserId",
                       onDelete: ReferentialAction.Cascade);
             });

         migrationBuilder.CreateTable(
             name: "CinemaUserRoles",
             columns: table => new {
                UserRoleId = table.Column<int>(type: "int", nullable: false)
                     .Annotation("SqlServer:Identity", "1, 1"),
                UserId = table.Column<int>(type: "int", nullable: false),
                RoleId = table.Column<int>(type: "int", nullable: false)
             },
             constraints: table => {
                table.PrimaryKey("PK_CinemaUserRoles", x => x.UserRoleId);
                table.ForeignKey(
                       name: "FK_CinemaUserRoles_CinemaRoles_RoleId",
                       column: x => x.RoleId,
                       principalTable: "CinemaRoles",
                       principalColumn: "RoleId",
                       onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                       name: "FK_CinemaUserRoles_Users_UserId",
                       column: x => x.UserId,
                       principalTable: "Users",
                       principalColumn: "UserId",
                       onDelete: ReferentialAction.Cascade);
             });

         migrationBuilder.CreateTable(
             name: "CinemaBookings",
             columns: table => new {
                BookingId = table.Column<int>(type: "int", nullable: false)
                     .Annotation("SqlServer:Identity", "1, 1"),
                UserId = table.Column<int>(type: "int", nullable: false),
                BroadcastId = table.Column<int>(type: "int", nullable: false),
                Seat = table.Column<int>(type: "int", nullable: false)
             },
             constraints: table => {
                table.PrimaryKey("PK_CinemaBookings", x => x.BookingId);
                table.ForeignKey(
                       name: "FK_CinemaBookings_CinemaBroadcasts_BroadcastId",
                       column: x => x.BroadcastId,
                       principalTable: "CinemaBroadcasts",
                       principalColumn: "BroadcastId",
                       onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                       name: "FK_CinemaBookings_Users_UserId",
                       column: x => x.UserId,
                       principalTable: "Users",
                       principalColumn: "UserId",
                       onDelete: ReferentialAction.Cascade);
             });

         migrationBuilder.CreateTable(
             name: "CinemaSeats",
             columns: table => new {
                SeatId = table.Column<int>(type: "int", nullable: false)
                     .Annotation("SqlServer:Identity", "1, 1"),
                BroadcastId = table.Column<int>(type: "int", nullable: false)
             },
             constraints: table => {
                table.PrimaryKey("PK_CinemaSeats", x => x.SeatId);
                table.ForeignKey(
                       name: "FK_CinemaSeats_CinemaBroadcasts_BroadcastId",
                       column: x => x.BroadcastId,
                       principalTable: "CinemaBroadcasts",
                       principalColumn: "BroadcastId",
                       onDelete: ReferentialAction.Cascade);
             });

         migrationBuilder.CreateIndex(
             name: "IX_AccessTokens_UserId",
             table: "AccessTokens",
             column: "UserId");

         migrationBuilder.CreateIndex(
             name: "IX_CinemaBookings_BroadcastId",
             table: "CinemaBookings",
             column: "BroadcastId");

         migrationBuilder.CreateIndex(
             name: "IX_CinemaBookings_UserId",
             table: "CinemaBookings",
             column: "UserId");

         migrationBuilder.CreateIndex(
             name: "IX_CinemaBroadcasts_CinemaLocationId",
             table: "CinemaBroadcasts",
             column: "CinemaLocationId");

         migrationBuilder.CreateIndex(
             name: "IX_CinemaBroadcasts_MovieId",
             table: "CinemaBroadcasts",
             column: "MovieId");

         migrationBuilder.CreateIndex(
             name: "IX_CinemaBroadcasts_PriceId",
             table: "CinemaBroadcasts",
             column: "PriceId");

         migrationBuilder.CreateIndex(
             name: "IX_CinemaReviews_MovieId",
             table: "CinemaReviews",
             column: "MovieId");

         migrationBuilder.CreateIndex(
             name: "IX_CinemaReviews_UserId",
             table: "CinemaReviews",
             column: "UserId");

         migrationBuilder.CreateIndex(
             name: "IX_CinemaSeats_BroadcastId",
             table: "CinemaSeats",
             column: "BroadcastId");

         migrationBuilder.CreateIndex(
             name: "IX_CinemaUserRoles_RoleId",
             table: "CinemaUserRoles",
             column: "RoleId");

         migrationBuilder.CreateIndex(
             name: "IX_CinemaUserRoles_UserId",
             table: "CinemaUserRoles",
             column: "UserId");

         migrationBuilder.CreateIndex(
             name: "IX_MovieActors_ActorId",
             table: "MovieActors",
             column: "ActorId");

         migrationBuilder.CreateIndex(
             name: "IX_MovieActors_MovieId",
             table: "MovieActors",
             column: "MovieId");
      }

      protected override void Down(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.DropTable(
             name: "AccessTokens");

         migrationBuilder.DropTable(
             name: "CinemaBookings");

         migrationBuilder.DropTable(
             name: "CinemaReviews");

         migrationBuilder.DropTable(
             name: "CinemaSeats");

         migrationBuilder.DropTable(
             name: "CinemaUserRoles");

         migrationBuilder.DropTable(
             name: "MovieActors");

         migrationBuilder.DropTable(
             name: "CinemaBroadcasts");

         migrationBuilder.DropTable(
             name: "CinemaRoles");

         migrationBuilder.DropTable(
             name: "Users");

         migrationBuilder.DropTable(
             name: "Actors");

         migrationBuilder.DropTable(
             name: "CinemaLocations");

         migrationBuilder.DropTable(
             name: "CinemaMovies");

         migrationBuilder.DropTable(
             name: "CinemaPrices");
      }
   }
}
