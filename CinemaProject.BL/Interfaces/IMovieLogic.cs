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
      bool CanRemoveReview(int userId, int reviewId);
      void RemoveReview(int id);
      List<string> GetLocationNames();
      List<CinemaBroadcastDTO> GetBroadcastsByMovieIdAndLocationName(int id, string locationName);
      int GetLocationIdByName(string locationName);
      void AddBroadcast(CinemaBroadcastDTO cinemaBroadcastDTO, int price);
      void DeleteBroadcast(int id);
      bool CanMakeReservation(string locationName, int movieId);
      void MakeReservation(int id, int numberOfSelectedSeats, int userId);
      List<CinemaBookingDTO> GetBookingsByUserIdAndLocationName(int userId, string locationName);
      void DeleteBooking(int id);
   }
}
