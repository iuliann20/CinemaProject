using System.Collections.Generic;

namespace CinemaProject.Models
{
   public class AllBroadcastsViewModel
   {
      public int MovieId { get; set; }
      public List<CinemaBroadcastViewModel> CinemaBroadcastViewModels { get; set; }
   }
}
