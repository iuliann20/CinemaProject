using CinemaProject.TL.DTO;
using System.Collections.Generic;

namespace CinemaProject.DAL.Repository.Interfaces
{
   public interface IMovieRepository
   {
      List<MovieDTO> GetAllMovies();
      int AddMovie(MovieDTO movieDTO);
      MovieDTO GetMovieById(int id);
      void RemoveMovie(int id);
      List<ReviewDTO> GetReviewsByMovieId(int movieId);
      void AddReview(ReviewDTO reviewDTO);
   }
}
