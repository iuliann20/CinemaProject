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
            foreach (var movieDTO in movieDTOs)
            {
                movieList.Add(new MovieViewModel
                {
                    MovieId = movieDTO.MovieId,
                    MovieName = movieDTO.MovieName,
                    Description = movieDTO.Description,
                    Duration = movieDTO.Duration,
                    MoviePhoto = movieDTO.MoviePhoto
                });
            }
            return movieList;
        }
        public MovieViewModel BuildViewModel(MovieDTO movieDTO)
        {
            return new MovieViewModel
            {
                MovieId = movieDTO.MovieId,
                MovieName = movieDTO.MovieName,
                Description = movieDTO.Description,
                Duration = movieDTO.Duration,
                MoviePhoto = movieDTO.MoviePhoto
            };
        }
        public MovieDTO BuildDTO(MovieViewModel movieViewModel)
        {
            return new MovieDTO
            {
                MovieId = movieViewModel.MovieId,
                MovieName = movieViewModel.MovieName,
                Description = movieViewModel.Description,
                Duration = movieViewModel.Duration,
                MoviePhoto = movieViewModel.MoviePhoto

            };
        }
    }
}
