using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaProject.DAL.Entities
{
   public class CinemaMovieActor
   {
      [Key]
      public int MovieActorId { get; set; }
      [ForeignKey("CinemaMovie")]
      public int MovieId { get; set; }
      [ForeignKey("CinemaActor")]
      public int ActorId { get; set; }
      public CinemaMovie CinemaMovie { get; set; }
      public CinemaActor CinemaActor { get; set; }


   }
}
