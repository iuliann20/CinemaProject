using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaProject.Models
{
   public class AllBroadcastsViewModel
   {
      public int MovieId { get; set; }
      public List<CinemaBroadcastViewModel> CinemaBroadcastViewModels { get; set; }
   }
}
