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
        private readonly IActorRepository _actorRepository;

        public MovieLogic(IMovieRepository movieRepository, IActorRepository actorRepository)
        {
            _movieRepository = movieRepository;
            _actorRepository = actorRepository;
        }

        public List<MovieDTO> GelAllMovies()
        {
            var allMovies=_movieRepository.GelAllMovies();
            return allMovies;
            
        }
        public void AddMovie(MovieDTO movieDTO)
        {
            _movieRepository.AddMovie(movieDTO);
            foreach (string actor in movieDTO.Actors)
            {
                _actorRepository.AddActor(new ActorDTO { ActorName = actor });
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
    }
}
