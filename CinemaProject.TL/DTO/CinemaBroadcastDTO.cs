using System;

namespace CinemaProject.TL.DTO
{
   public class CinemaBroadcastDTO
   {
      public int BroadcastId { get; set; }
      public int MovieId { get; set; }
      public int CinemaLocationId { get; set; }
      public int PriceId { get; set; }
      public int NumberOfSeats { get; set; }
      public DateTime Time { get; set; }
      public CinemaLocationDTO CinemaLocationDTO { get; set; }
      public PriceDTO PriceDTO { get; set; }
   }
}
