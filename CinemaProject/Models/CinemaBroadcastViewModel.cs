using CinemaProject.TL.DTO;
using System;

namespace CinemaProject.Models
{
   public class CinemaBroadcastViewModel
   {
      public int BroadcastId { get; set; }
      public int MovieId { get; set; }
      public int CinemaLocationId { get; set; }
      public int PriceId { get; set; }
      public int Price { get; set; }
      public int NumberOfSeats { get; set; }
      public int NumberOfSelectedSeats { get; set; }
      public DateTime Time { get; set; }
      public CinemaLocationDTO CinemaLocationDTO { get; set; }
      public PriceDTO PriceDTO { get; set; }
   }
}
