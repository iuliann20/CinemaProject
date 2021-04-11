using CinemaProject.Helpers.Interfaces;
using CinemaProject.Models;
using CinemaProject.TL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaProject.Helpers.Classes
{
   public class MovieControllerHelper : IMovieControllerHelper
   {
      public List<MovieViewModel> BuildListViewModel(List<MovieDTO> movieDTOs)
      {
         List<MovieViewModel> movieList = new List<MovieViewModel>();
         foreach (var movieDTO in movieDTOs) {
            movieList.Add(new MovieViewModel {
               MovieId = movieDTO.MovieId,
               MovieName = movieDTO.MovieName,
               Description = movieDTO.Description,
               Duration = movieDTO.Duration,
               MoviePhoto = movieDTO.MoviePhoto,
               Actors = movieDTO.Actors
            });
         }
         return movieList;
      }
      public MovieViewModel BuildViewModel(MovieDTO movieDTO)
      {
         return new MovieViewModel {
            MovieId = movieDTO.MovieId,
            MovieName = movieDTO.MovieName,
            Description = movieDTO.Description,
            Duration = movieDTO.Duration,
            MoviePhoto = movieDTO.MoviePhoto,
            Actors = movieDTO.Actors

         };
      }
      public MovieDTO BuildDTO(MovieViewModel movieViewModel)
      {
         return new MovieDTO {
            MovieId = movieViewModel.MovieId,
            MovieName = movieViewModel.MovieName,
            Description = movieViewModel.Description,
            Duration = movieViewModel.Duration,
            MoviePhoto = movieViewModel.Photo.FileName,
            Actors = movieViewModel.Actors
         };
      }

      public List<string> VerifyViewModel(MovieViewModel movieViewModel)
      {
         var listOfErrors = new List<string>();
         if (string.IsNullOrWhiteSpace(movieViewModel.MovieName)) {
            listOfErrors.Add("Enter a movie name.");
         }
         if (string.IsNullOrWhiteSpace(movieViewModel.Description)) {
            listOfErrors.Add("Enter a movie description.");
         }
         if (movieViewModel.Duration == 0) {
            listOfErrors.Add("Enter a movie duration.");
         }
         if (movieViewModel.Actors == null || !movieViewModel.Actors.Any() || movieViewModel.Actors.Count <= 1) {
            listOfErrors.Add("Enter at least 2 actors.");
         }
         return listOfErrors;
      }
   }
}
