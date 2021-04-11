using System.ComponentModel.DataAnnotations;

namespace CinemaProject.DAL.Entities
{
   public class CinemaActor
   {
      [Key]
      public int ActorId { get; set; }
      [MaxLength(25)]
      public string ActorName { get; set; }
   }
}
