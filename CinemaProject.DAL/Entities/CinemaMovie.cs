using System.ComponentModel.DataAnnotations;

namespace CinemaProject.DAL.Entities
{
   public class CinemaMovie
   {
      [Key]
      public int MovieId { get; set; }
      [MaxLength(100)]
      public string MovieName { get; set; }
      public string Description { get; set; }
      public int Duration { get; set; }
      public string MoviePhoto { get; set; }



   }
}
