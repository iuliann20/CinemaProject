using CinemaProject.Models;
using CinemaProject.TL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaProject.Helpers.Interfaces
{
   public interface IMovieControllerHelper
   {
      List<MovieViewModel> BuildListViewModel(List<MovieDTO> movieDTOs);
      MovieViewModel BuildViewModel(MovieDTO movieDTO);
      MovieDTO BuildDTO(MovieViewModel movieViewModel);
      List<string> VerifyViewModel(MovieViewModel movieViewModel);
   }
}
