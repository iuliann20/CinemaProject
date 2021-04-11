using System.ComponentModel.DataAnnotations;

namespace CinemaProject.DAL.Entities
{
   public class CinemaRole
   {
      [Key]
      public int RoleId { get; set; }
      public string Role { get; set; }

   }
}
