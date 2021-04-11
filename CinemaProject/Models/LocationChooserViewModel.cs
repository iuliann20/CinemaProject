using System.Collections.Generic;

namespace CinemaProject.Models
{
   public class LocationChooserViewModel
   {
      public string SelectedLocation { get; set; }
      public IList<string> LocationNames { get; set; }
   }
}
