using CinemaProject.BL.Interfaces;
using CinemaProject.DAL.Repository.Interfaces;
using CinemaProject.TL.DTO;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CinemaProject.BL.Classes
{
   public class MovieLogic : IMovieLogic
   {
      private readonly IMovieRepository _movieRepository;
      private readonly IActorRepository _actorRepository;

      public MovieLogic(IMovieRepository movieRepository, IActorRepository actorRepository)
      {
         _movieRepository = movieRepository;
         _actorRepository = actorRepository;
      }

      public List<MovieDTO> GetAllMovies()
      {
         List<MovieDTO> allMovies = _movieRepository.GetAllMovies();
         return allMovies;

      }
      public void AddMovie(MovieDTO movieDTO)
      {
         int movieId = _movieRepository.AddMovie(movieDTO);
         if (movieDTO.Actors == null || !movieDTO.Actors.Any()) {
            return;
         }
         List<int> actorsIds = new List<int>();
         foreach (string actor in movieDTO.Actors) {
            ActorDTO actorFromDb = _actorRepository.GetActorByName(actor);
            if (actorFromDb == null) {
               actorsIds.Add(_actorRepository.AddActor(new ActorDTO { ActorName = actor }));
            } else {
               actorsIds.Add(actorFromDb.ActorId);
            }
         }
         foreach (int actorId in actorsIds) {
            _actorRepository.AddActorByMovieId(actorId, movieId);
         }

      }
      public MovieDTO GetMovieById(int id)
      {
         return _movieRepository.GetMovieById(id);
      }
      public void RemoveMovie(int id)
      {
         _movieRepository.RemoveMovie(id);
      }

      public void UploadMoviePhoto(string uploadFolder, IFormFile photo)
      {
         string filePath = Path.Combine(uploadFolder, photo.FileName);
         using (Stream fileStream = new FileStream(filePath, FileMode.Create)) {
            photo.CopyToAsync(fileStream);
         }
      }

      public List<ReviewDTO> GetReviewsByMovieId(int movieId)
      {
         return _movieRepository.GetReviewsByMovieId(movieId);
      }

      public bool AddReview(ReviewDTO reviewDTO)
      {
         if (reviewDTO != null) {
            if (reviewDTO.MovieId == 0 || reviewDTO.UserId == 0 || string.IsNullOrWhiteSpace(reviewDTO.Review)) {
               return false;
            }
         }
         _movieRepository.AddReview(reviewDTO);
         return true;
      }

      public bool CanRemoveReview(int userId, int reviewId)
      {
         if (userId != 0 && reviewId != 0) {
            ReviewDTO reviewDto = _movieRepository.GetReviewByReviewId(reviewId);
            if (reviewDto != null) {
               return reviewDto.UserId == userId;
            }
            return false;
         }
         return false;
      }

      public void RemoveReview(int id)
      {
         _movieRepository.RemoveReview(id);
      }

      public List<string> GetLocationNames()
      {
         return _movieRepository.GetLocationNames();
      }

      public List<CinemaBroadcastDTO> GetBroadcastsByMovieIdAndLocationName(int id, string locationName)
      {
         List<CinemaBroadcastDTO> broadcastDTOs = _movieRepository.GetBroadcastsByMovieIdAndLocationName(id, locationName);
         foreach (CinemaBroadcastDTO broadcastDTO in broadcastDTOs) {
            broadcastDTO.CinemaLocationDTO = _movieRepository.GetLocationById(broadcastDTO.CinemaLocationId);
            broadcastDTO.PriceDTO = _movieRepository.GetPriceById(broadcastDTO.PriceId);
         }
         return broadcastDTOs;
      }

      public int GetLocationIdByName(string locationName)
      {
         return _movieRepository.GetLocationIdByName(locationName);
      }

      public void AddBroadcast(CinemaBroadcastDTO cinemaBroadcastDTO, int price)
      {
         int priceId = _movieRepository.GetOrAddPriceInDb(price);
         cinemaBroadcastDTO.PriceId = priceId;
         _movieRepository.AddBroadcast(cinemaBroadcastDTO);
      }

      public void DeleteBroadcast(int id)
      {
         _movieRepository.DeleteBroadcast(id);
      }

      public bool CanMakeReservation(string locationName, int movieId)
      {
         List<CinemaBroadcastDTO> broadcasts = _movieRepository.GetBroadcastsByMovieIdAndLocationName(movieId, locationName);
         if (broadcasts.Any()) {
            return true;
         }
         return false;
      }

      public void MakeReservation(int id, int numberOfSelectedSeats, int userId)
      {
         _movieRepository.MakeBooking(id, userId, numberOfSelectedSeats);
         _movieRepository.UpdateSeats(id, numberOfSelectedSeats);
      }

      public List<CinemaBookingDTO> GetBookingsByUserIdAndLocationName(int userId, string locationName)
      {
         List<CinemaBookingDTO> bookings = _movieRepository.GetBookingsByUserId(userId);

         foreach (CinemaBookingDTO booking in bookings) {
            CinemaBroadcastDTO broadcast = _movieRepository.GetBroadcastByIdAndLocationName(booking.BookingId, locationName);
            string movieName = _movieRepository.GetMovieById(broadcast.MovieId).MovieName;
            int price = _movieRepository.GetPriceById(broadcast.PriceId).Price;
            booking.MovieName = movieName;
            booking.Price = price * booking.Seat;
            booking.AvalableSeats = broadcast.NumberOfSeats;
            booking.CinemaName = locationName;
            booking.Time = broadcast.Time;
            booking.MovieId = broadcast.MovieId;
         }
         return bookings;
      }

      public void DeleteBooking(int id)
      {
         _movieRepository.DeleteBooking(id);
      }
   }
}
