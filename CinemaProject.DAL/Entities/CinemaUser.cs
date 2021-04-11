using System;
using System.ComponentModel.DataAnnotations;

namespace CinemaProject.DAL.Entities
{
   public class CinemaUser
   {
      [Key]
      public int UserId { get; set; }
      [MaxLength(20)]
      public string FirstName { get; set; }
      [MaxLength(20)]
      public string LastName { get; set; }
      public DateTime BirthDay { get; set; }
      [MaxLength(100)]
      public string Email { get; set; }
      [MaxLength(20)]
      public string PhoneNumber { get; set; }
      public string Password { get; set; }
   }
}
