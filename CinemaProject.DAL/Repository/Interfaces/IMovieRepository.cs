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
      ReviewDTO GetReviewByReviewId(int reviewId);
      void RemoveReview(int id);
      List<string> GetLocationNames();
      List<CinemaBroadcastDTO> GetBroadcastsByMovieIdAndLocationName(int id, string locationName);
      CinemaLocationDTO GetLocationById(int cinemaLocationId);
      PriceDTO GetPriceById(int priceId);
      int GetLocationIdByName(string locationName);
      int GetOrAddPriceInDb(int price);
      void AddBroadcast(CinemaBroadcastDTO cinemaBroadcastDTO);
      void DeleteBroadcast(int id);
      void MakeBooking(int id, int userId, int numberOfSelectedSeats);
      void UpdateSeats(int id, int numberOfSelectedSeats);
      List<CinemaBookingDTO> GetBookingsByUserId(int userId);
      CinemaBroadcastDTO GetBroadcastByIdAndLocationName(int bookingId, string locationName);
      void DeleteBooking(int id);
   }
}
