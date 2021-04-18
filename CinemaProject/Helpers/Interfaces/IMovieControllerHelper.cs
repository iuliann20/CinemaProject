using CinemaProject.Models;
using CinemaProject.TL.DTO;
using System.Collections.Generic;

namespace CinemaProject.Helpers.Interfaces
{
   public interface IMovieControllerHelper
   {
      List<MovieViewModel> BuildListViewModel(List<MovieDTO> movieDTOs);
      MovieViewModel BuildViewModel(MovieDTO movieDTO);
      MovieDTO BuildDTO(MovieViewModel movieViewModel);
      CinemaBroadcastDTO BuildDTO(CinemaBroadcastViewModel cinemaBroadcastViewModel);
      List<string> VerifyViewModel(MovieViewModel movieViewModel);
   }
}
