using CinemaProject.DAL.Entities;
using CinemaProject.DAL.Repository.Interfaces;
using CinemaProject.TL.DTO;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CinemaProject.DAL.Repository.Classes
{
   public class MovieRepository : IMovieRepository
   {
      private readonly CinemaDbContext _cinemaDbContext;

      public MovieRepository(CinemaDbContext cinemaDbContext)
      {
         _cinemaDbContext = cinemaDbContext;
      }
      public List<MovieDTO> GetAllMovies()
      {
         List<MovieDTO> movies = _cinemaDbContext.CinemaMovies.Select(movie => new MovieDTO {
            MovieId = movie.MovieId,
            MovieName = movie.MovieName,
            Description = movie.Description,
            Duration = movie.Duration,
            MoviePhoto = movie.MoviePhoto,
         }).ToList();
         return movies;
      }
      public int AddMovie(MovieDTO movieDTO)
      {
         CinemaMovie movie = new CinemaMovie {
            MovieName = movieDTO.MovieName,
            Description = movieDTO.Description,
            Duration = movieDTO.Duration,
            MoviePhoto = movieDTO.MoviePhoto
         };
         _cinemaDbContext.CinemaMovies.Add(movie);
         _cinemaDbContext.SaveChanges();
         return movie.MovieId;
      }
      public MovieDTO GetMovieById(int id)
      {
         CinemaMovie movieById = _cinemaDbContext.CinemaMovies.FirstOrDefault(x => x.MovieId == id);
         return new MovieDTO {
            MovieId = movieById.MovieId,
            MovieName = movieById.MovieName,
            Description = movieById.Description,
            Duration = movieById.Duration,
            MoviePhoto = movieById.MoviePhoto,
            Actors = _cinemaDbContext.MovieActors
            .Include(x => x.CinemaActor)
            .Where(x => x.MovieId == movieById.MovieId)
            .Select(actor => actor.CinemaActor.ActorName)
            .ToList()
         };
      }
      public void RemoveMovie(int id)
      {
         CinemaMovie movieFromDb = _cinemaDbContext.CinemaMovies.FirstOrDefault(x => x.MovieId == id);
         if (movieFromDb != null) {
            _cinemaDbContext.Remove(movieFromDb);
            _cinemaDbContext.SaveChanges();
         }

      }

      public List<ReviewDTO> GetReviewsByMovieId(int movieId)
      {
         return _cinemaDbContext.CinemaReviews
            .Where(x => x.MovieId == movieId)
            .Select(review => new ReviewDTO {
               ReviewId = review.ReviewId,
               UserId = review.UserId,
               MovieId = review.MovieId,
               Review = review.Review,
               UserFirstName = _cinemaDbContext.Users.FirstOrDefault(x => x.UserId == review.UserId).FirstName
            }).ToList();
      }

      public void AddReview(ReviewDTO reviewDTO)
      {
         _cinemaDbContext.CinemaReviews.Add(new CinemaReview {
            MovieId = reviewDTO.MovieId,
            UserId = reviewDTO.UserId,
            Review = reviewDTO.Review
         });
         _cinemaDbContext.SaveChanges();
      }

      public ReviewDTO GetReviewByReviewId(int reviewId)
      {
         CinemaReview reviewFromDB = _cinemaDbContext.CinemaReviews.FirstOrDefault(x => x.ReviewId == reviewId);
         return new ReviewDTO {
            MovieId = reviewFromDB.MovieId,
            Review = reviewFromDB.Review,
            ReviewId = reviewFromDB.ReviewId,
            UserId = reviewFromDB.UserId,
         };
      }

      public void RemoveReview(int id)
      {
         CinemaReview reviewFromDb = _cinemaDbContext.CinemaReviews.FirstOrDefault(x => x.ReviewId == id);
         if (reviewFromDb != null) {
            _cinemaDbContext.Remove(reviewFromDb);
            _cinemaDbContext.SaveChanges();
         }
      }

      public List<string> GetLocationNames()
      {
         return _cinemaDbContext.CinemaLocations.Select(x => x.NameLocation).OrderBy(x => x).ToList();
      }

      public List<CinemaBroadcastDTO> GetBroadcastsByMovieIdAndLocationName(int id, string locationName)
      {
         CinemaLocation location = _cinemaDbContext.CinemaLocations.FirstOrDefault(x => x.NameLocation == locationName);
         if (location == null) {
            return null;
         }
         List<CinemaBroadcastDTO> broadcastsFromDb = _cinemaDbContext.CinemaBroadcasts
            .Where(x => x.MovieId == id && x.CinemaLocationId == location.LocationId)
            .Select(x => new CinemaBroadcastDTO {
               BroadcastId = x.BroadcastId,
               CinemaLocationId = x.CinemaLocationId,
               MovieId = x.MovieId,
               PriceId = x.PriceId,
               Time = x.BroadcastTime,
               NumberOfSeats = x.NumberOfSeats
            }).ToList();
         return broadcastsFromDb;
      }

      public CinemaLocationDTO GetLocationById(int cinemaLocationId)
      {
         CinemaLocation locationFromDb = _cinemaDbContext.CinemaLocations.FirstOrDefault(x => x.LocationId == cinemaLocationId);
         if (locationFromDb == null) {
            return null;
         }
         return new CinemaLocationDTO {
            LocationId = locationFromDb.LocationId,
            AddressLocation = locationFromDb.AddressLocation,
            NameLocation = locationFromDb.NameLocation,
         };
      }

      public PriceDTO GetPriceById(int priceId)
      {
         CinemaPrice priceFromDb = _cinemaDbContext.CinemaPrices.FirstOrDefault(x => x.PriceId == priceId);
         if (priceFromDb == null) {
            return null;
         }
         return new PriceDTO {
            PriceId = priceFromDb.PriceId,
            Price = priceFromDb.Price,
         };
      }

      public int GetLocationIdByName(string locationName)
      {
         CinemaLocation location = _cinemaDbContext.CinemaLocations.FirstOrDefault(x => x.NameLocation == locationName);
         if (location == null) {
            return 0;
         }
         return location.LocationId;
      }

      public int GetOrAddPriceInDb(int price)
      {
         CinemaPrice priceFromDb = _cinemaDbContext.CinemaPrices.FirstOrDefault(x => x.Price == price);
         if (priceFromDb != null) {
            return priceFromDb.PriceId;
         }
         CinemaPrice insertPrice = new CinemaPrice { Price = price };
         _cinemaDbContext.CinemaPrices.Add(insertPrice);
         _cinemaDbContext.SaveChanges();
         return insertPrice.PriceId;
      }

      public void AddBroadcast(CinemaBroadcastDTO cinemaBroadcastDTO)
      {
         CinemaBroadcast insertBroadcast = new CinemaBroadcast {
            MovieId = cinemaBroadcastDTO.MovieId,
            CinemaLocationId = cinemaBroadcastDTO.CinemaLocationId,
            PriceId = cinemaBroadcastDTO.PriceId,
            NumberOfSeats = cinemaBroadcastDTO.NumberOfSeats,
            BroadcastTime = cinemaBroadcastDTO.Time,
         };
         _cinemaDbContext.CinemaBroadcasts.Add(insertBroadcast);
         _cinemaDbContext.SaveChanges();
      }

      public void DeleteBroadcast(int id)
      {
         CinemaBroadcast broadcastFromDb = _cinemaDbContext.CinemaBroadcasts.FirstOrDefault(x => x.BroadcastId == id);
         if (broadcastFromDb != null) {
            _cinemaDbContext.CinemaBroadcasts.Remove(broadcastFromDb);
            _cinemaDbContext.SaveChanges();
         }
      }

      public void MakeBooking(int id, int userId, int numberOfSelectedSeats)
      {
         CinemaBooking insertBooking = new CinemaBooking {
            BroadcastId = id,
            UserId = userId,
            Seat = numberOfSelectedSeats,
         };
         _cinemaDbContext.CinemaBookings.Add(insertBooking);
         _cinemaDbContext.SaveChanges();
      }

      public void UpdateSeats(int id, int numberOfSelectedSeats)
      {
         CinemaBroadcast broadcast = _cinemaDbContext.CinemaBroadcasts.FirstOrDefault(x => x.BroadcastId == id);
         broadcast.NumberOfSeats = broadcast.NumberOfSeats - numberOfSelectedSeats;
         _cinemaDbContext.SaveChanges();
      }

      public List<CinemaBookingDTO> GetBookingsByUserId(int userId)
      {
         List<CinemaBookingDTO> bookingFromDb = _cinemaDbContext.CinemaBookings
            .Where(x => x.UserId == userId)
            .Select(x => new CinemaBookingDTO {
               BookingId = x.BookingId,
               BroadcastId = x.BroadcastId,
               UserId = x.UserId,
               Seat = x.Seat,
            }).ToList();
         return bookingFromDb;
      }

      public CinemaBroadcastDTO GetBroadcastByIdAndLocationName(int broadcastId, string locationName)
      {
         CinemaBroadcast broadcastFromDb = _cinemaDbContext.CinemaBroadcasts
            .Include(x => x.CinemaLocation)
            .FirstOrDefault(x => x.BroadcastId == broadcastId && x.CinemaLocation.NameLocation == locationName);

         return new CinemaBroadcastDTO {
            BroadcastId = broadcastFromDb.BroadcastId,
            MovieId = broadcastFromDb.MovieId,
            PriceId = broadcastFromDb.PriceId,
            CinemaLocationId = broadcastFromDb.CinemaLocationId,
            NumberOfSeats = broadcastFromDb.NumberOfSeats,
            Time = broadcastFromDb.BroadcastTime,
         };
      }

      public void DeleteBooking(int id)
      {
         CinemaBooking bookingFromDb = _cinemaDbContext.CinemaBookings.FirstOrDefault(x => x.BookingId == id);
         if (bookingFromDb != null) {
            int seats = bookingFromDb.Seat;
            int broadcastId = bookingFromDb.BroadcastId;
            _cinemaDbContext.CinemaBookings.Remove(bookingFromDb);

            CinemaBroadcast broadcastFromDb = _cinemaDbContext.CinemaBroadcasts.FirstOrDefault(x => x.BroadcastId == broadcastId);
            broadcastFromDb.NumberOfSeats += seats;
            _cinemaDbContext.SaveChanges();
         }
      }
   }
}
