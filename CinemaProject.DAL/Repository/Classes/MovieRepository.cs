using CinemaProject.DAL.Entities;
using CinemaProject.DAL.Repository.Interfaces;
using CinemaProject.TL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaProject.DAL.Repository.Classes
{
    public class MovieRepository : IMovieRepository
    {
        private readonly CinemaDbContext _cinemaDbContext;

        public MovieRepository(CinemaDbContext cinemaDbContext)
        {
            _cinemaDbContext = cinemaDbContext;
        }
        public List<MovieDTO> GelAllMovies()
        {
            var movies = _cinemaDbContext.CinemaMovies.Select(movie => new MovieDTO
            {
                MovieId = movie.MovieId,
                MovieName = movie.MovieName,
                Description = movie.Description,
                Duration = movie.Duration
            }).ToList();
            return movies;
        }
        public void AddMovie(MovieDTO movieDTO)
        {
            _cinemaDbContext.CinemaMovies.Add(new CinemaMovie
            {
                MovieName = movieDTO.MovieName,
                Description = movieDTO.Description,
                Duration = movieDTO.Duration,
                MoviePhoto=movieDTO.MoviePhoto
            });
            _cinemaDbContext.SaveChanges();
        }
    }
}
