using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaProject.DAL.Entities
{
    public class CinemaBooking
    {
        [Key]
        public int BookingId { get; set; }
        [ForeignKey("CinemaUser")]
        public int UserId { get; set; }
        [ForeignKey("CinemaBroadcast")]
        public int BroadcastId { get; set; }
        public int Seat { get; set; }
        public CinemaUser CinemaUser { get; set; }
        public CinemaBroadcast CinemaBroadcast { get; set; }


    }
}
