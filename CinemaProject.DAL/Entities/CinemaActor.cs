using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaProject.DAL.Entities
{
    public class CinemaActor
    {
        [Key]
        public int ActorId { get; set; }
        [MaxLength(25)]
        public string ActorName { get; set; }
    }
}
