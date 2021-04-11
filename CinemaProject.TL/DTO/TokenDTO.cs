using System;

namespace CinemaProject.TL.DTO
{
   public class TokenDTO
   {
      public int TokenId { get; set; }
      public string AccessToken { get; set; }
      public int UserId { get; set; }
      public DateTime ExpirationDate { get; set; }
   }
}
