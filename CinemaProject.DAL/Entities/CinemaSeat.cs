using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaProject.DAL.Entities
{
   public class CinemaSeat
   {
      [Key]
      public int SeatId { get; set; }
      [ForeignKey("CinemaBroadcast")]
      public int BroadcastId { get; set; }
      public CinemaBroadcast CinemaBroadcast { get; set; }

   }
}
