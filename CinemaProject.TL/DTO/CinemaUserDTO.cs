﻿using System;

namespace CinemaProject.TL.DTO
{
   public class CinemaUserDTO
   {
      public int UserId { get; set; }
      public string FirstName { get; set; }
      public string LastName { get; set; }
      public DateTime BirthDay { get; set; }
      public string Email { get; set; }
      public string PhoneNumber { get; set; }

      public string Password { get; set; }
      public string RoleName { get; set; }
   }
}
