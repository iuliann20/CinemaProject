using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaProject.DAL.Entities
{
    public class CinemaBroadcast
    {
        [Key]
        public int BroadcastId { get; set; }
        [ForeignKey("CinemaMovie")]
        public int MovieId { get; set; }
        [ForeignKey("CinemaLocation")]
        public int CinemaLocationId { get; set; }
        [ForeignKey("CinemaPrice")]
        public int PriceId { get; set; }
        public int Time { get; set; }
        public CinemaMovie CinemaMovie { get; set; }
        public CinemaLocation CinemaLocation { get; set; }
        public CinemaPrice CinemaPrice { get; set; }





    }
}
