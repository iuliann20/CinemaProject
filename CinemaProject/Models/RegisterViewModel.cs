using System;

namespace CinemaProject.Models
{
   public class RegisterViewModel
   {
      public int UserId { get; set; }
      public string FirstName { get; set; }
      public string LastName { get; set; }
      public DateTime BirthDay { get; set; }
      public string Email { get; set; }
      public string PhoneNumber { get; set; }

      public string Password { get; set; }
      public string RePassword { get; set; }
   }
}
