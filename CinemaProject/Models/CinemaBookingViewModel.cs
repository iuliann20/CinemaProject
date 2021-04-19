using System;

namespace CinemaProject.Models
{
   public class CinemaBookingViewModel
   {
      public int BookingId { get; set; }
      public int UserId { get; set; }
      public int MovieId { get; set; }
      public int BroadcastId { get; set; }
      public int Seat { get; set; }
      public string MovieName { get; set; }
      public int Price { get; set; }
      public int AvalableSeats { get; set; }
      public string CinemaName { get; set; }
      public DateTime Time { get; set; }
   }
}
