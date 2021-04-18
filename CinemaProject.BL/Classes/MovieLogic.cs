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
            var reviewDto = _movieRepository.GetReviewByReviewId(reviewId);
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
         var broadcastDTOs = _movieRepository.GetBroadcastsByMovieIdAndLocationName(id, locationName);
         foreach (var broadcastDTO in broadcastDTOs) {
            broadcastDTO.CinemaLocationDTO = _movieRepository.GetLocationById(broadcastDTO.CinemaLocationId);
            broadcastDTO.PriceDTO = _movieRepository.GetPriceById(broadcastDTO.PriceId);
         }
         return broadcastDTOs;
      }

      public object GetLocationNameById(string locationName)
      {
         throw new System.NotImplementedException();
      }
   }
}
