using CinemaProject.BL.Interfaces;
using CinemaProject.DAL.Repository.Interfaces;
using CinemaProject.TL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaProject.BL.Classes
{
    public class MovieLogic:IMovieLogic
    {
        private readonly IMovieRepository _movieRepository;

        public MovieLogic(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public List<MovieDTO> GelAllMovies()
        {
            var allMovies=_movieRepository.GelAllMovies();
            return allMovies;
            
        }
        public void AddMovie(MovieDTO movieDTO)
        {
            _movieRepository.AddMovie(movieDTO);
        }
        public MovieDTO GetMovieById(int id)
        {
            return _movieRepository.GetMovieById(id);
        }
    }
}
