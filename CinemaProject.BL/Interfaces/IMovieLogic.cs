using CinemaProject.TL.DTO;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace CinemaProject.BL.Interfaces
{
   public interface IMovieLogic
   {
      List<MovieDTO> GetAllMovies();
      void AddMovie(MovieDTO movieDTO);
      MovieDTO GetMovieById(int id);
      void RemoveMovie(int id);
      void UploadMoviePhoto(string uploadFolder, IFormFile photo);
      List<ReviewDTO> GetReviewsByMovieId(int movieId);
      bool AddReview(ReviewDTO reviewDTO);
   }
}
