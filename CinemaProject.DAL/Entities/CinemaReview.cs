using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaProject.DAL.Entities
{
   public class CinemaReview
   {
      [Key]
      public int ReviewId { get; set; }
      [ForeignKey("CinemaUser")]
      public int UserId { get; set; }
      [ForeignKey("CinemaMovie")]
      public int MovieId { get; set; }
      public string Review { get; set; }
      public CinemaUser CinemaUser { get; set; }
      public CinemaMovie CinemaMovie { get; set; }
   }
}
