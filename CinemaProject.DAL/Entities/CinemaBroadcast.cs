using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaProject.DAL.Entities
{
   public class CinemaBroadcast
   {
      [Key]
      public int BroadcastId { get; set; }
      [ForeignKey("CinemaMovie")]
      public int MovieId { get; set; }
      [ForeignKey("CinemaLocation")]
      public int CinemaLocationId { get; set; }
      [ForeignKey("CinemaPrice")]
      public int PriceId { get; set; }
      public int NumberOfSeats { get; set; }
      public DateTime BroadcastTime { get; set; }
      public CinemaMovie CinemaMovie { get; set; }
      public CinemaLocation CinemaLocation { get; set; }
      public CinemaPrice CinemaPrice { get; set; }
   }
}
