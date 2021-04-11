using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaProject.DAL.Entities
{
   public class Token
   {
      [Key]
      public int TokenId { get; set; }
      public string AccessToken { get; set; }
      public DateTime ExpirationDate { get; set; }
      [ForeignKey("CinemaUser")]
      public int UserId { get; set; }
      public CinemaUser User { get; set; }
   }
}
