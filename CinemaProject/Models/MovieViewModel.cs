﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaProject.Models
{
   public class MovieViewModel
   {
      public int MovieId { get; set; }
      public string MovieName { get; set; }
      public string Description { get; set; }
      public int Duration { get; set; }
      public List<string> Actors { get; set; }
      public string MoviePhoto { get; set; }
      public IFormFile Photo { get; set; }
   }
}
