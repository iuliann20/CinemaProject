using System.ComponentModel.DataAnnotations;

namespace CinemaProject.DAL.Entities
{
   public class CinemaLocation
   {
      [Key]
      public int LocationId { get; set; }
      [MaxLength(255)]
      public string NameLocation { get; set; }
      [MaxLength(255)]
      public string AddressLocation { get; set; }

   }
}
