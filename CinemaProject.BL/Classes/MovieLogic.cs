using CinemaProject.BL.Interfaces;
using CinemaProject.DAL.Repository.Interfaces;
using CinemaProject.TL.DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
         var allMovies = _movieRepository.GetAllMovies();
         return allMovies;

      }
      public void AddMovie(MovieDTO movieDTO)
      {
         var movieId = _movieRepository.AddMovie(movieDTO);
         if (movieDTO.Actors == null || !movieDTO.Actors.Any()) {
            return;
         }
         var actorsIds = new List<int>();
         foreach (string actor in movieDTO.Actors) {
            var actorFromDb = _actorRepository.GetActorByName(actor);
            if (actorFromDb == null) {
               actorsIds.Add(_actorRepository.AddActor(new ActorDTO { ActorName = actor }));
            } else {
               actorsIds.Add(actorFromDb.ActorId);
            }
         }
         foreach (var actorId in actorsIds) {
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
   }
}
