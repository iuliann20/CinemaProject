using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaProject.DAL.Entities
{
   public class CinemaUserRole
   {
      [Key]
      public int UserRoleId { get; set; }
      [ForeignKey("CinemaUser")]
      public int UserId { get; set; }
      [ForeignKey("CinemaRole")]
      public int RoleId { get; set; }
      public CinemaUser CinemaUser { get; set; }
      public CinemaRole CinemaRole { get; set; }
   }
}
