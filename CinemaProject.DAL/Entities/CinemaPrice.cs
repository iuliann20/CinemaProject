using System.ComponentModel.DataAnnotations;

namespace CinemaProject.DAL.Entities
{
   public class CinemaPrice
   {
      [Key]
      public int PriceId { get; set; }
      public int Price { get; set; }

   }
}
