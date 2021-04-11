using CinemaProject.TL.DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaProject.BL.Interfaces
{
   public interface IMovieLogic
   {
      List<MovieDTO> GetAllMovies();
      void AddMovie(MovieDTO movieDTO);
      MovieDTO GetMovieById(int id);
      void RemoveMovie(int id);
      void UploadMoviePhoto(string uploadFolder, IFormFile photo);
   }
}
