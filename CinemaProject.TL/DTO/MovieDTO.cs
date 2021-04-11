using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaProject.TL.DTO
{
    public class MovieDTO
    {
        public int MovieId { get; set; }
        public string MovieName { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public string MoviePhoto { get; set; }
        public List<string> Actors { get; set; }
    }
}
