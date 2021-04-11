namespace CinemaProject.TL.DTO
{
   public class ReviewDTO
   {
      public int ReviewId { get; set; }
      public int UserId { get; set; }
      public int MovieId { get; set; }
      public string Review { get; set; }
      public string UserFirstName { get; set; }
   }
}
