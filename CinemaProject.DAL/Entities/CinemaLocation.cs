using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaProject.DAL.Entities
{
    public class CinemaLocation
    {
        [Key]
        public int LocationId { get; set; }
        [MaxLength(255)]
        public string NameLocation { get; set; }
        [MaxLength(255)]
        public string AddressLocation { get; set; }

    }
}
