using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaProject.Models
{
    public class LocationChooserViewModel
    {
        public string SelectedLocation { get; set; }
        public IList<string> LocationNames { get; set; }
    }
}
