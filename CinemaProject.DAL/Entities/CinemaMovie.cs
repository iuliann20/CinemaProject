using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaProject.DAL.Entities
{
    public class CinemaMovie
    {
        [Key]
        public int MovieId { get; set; }
        [MaxLength(25)]
        public string MovieName { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }



    }
}
