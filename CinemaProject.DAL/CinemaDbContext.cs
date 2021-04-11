using CinemaProject.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CinemaProject.DAL
{
   public class CinemaDbContext : DbContext
   {
      public CinemaDbContext(DbContextOptions<CinemaDbContext> options) : base(options)
      {
      }

      public DbSet<CinemaUser> Users { get; set; }
      public DbSet<Token> AccessTokens { get; set; }
      public DbSet<CinemaActor> Actors { get; set; }
      public DbSet<CinemaLocation> CinemaLocations { get; set; }
      public DbSet<CinemaMovie> CinemaMovies { get; set; }
      public DbSet<CinemaMovieActor> MovieActors { get; set; }
      public DbSet<CinemaPrice> CinemaPrices { get; set; }
      public DbSet<CinemaReview> CinemaReviews { get; set; }
      public DbSet<CinemaRole> CinemaRoles { get; set; }
      public DbSet<CinemaSeat> CinemaSeats { get; set; }
      public DbSet<CinemaUserRole> CinemaUserRoles { get; set; }
      public DbSet<CinemaBooking> CinemaBookings { get; set; }
      public DbSet<CinemaBroadcast> CinemaBroadcasts { get; set; }













   }
}

