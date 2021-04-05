using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaProject.DAL.Entities
{
    public class CinemaSeat
    {
        [Key]
        public int SeatId { get; set; }
        [ForeignKey("CinemaBroadcast")]
        public int BroadcastId { get; set; }
        public CinemaBroadcast CinemaBroadcast { get; set; }

    }
}
