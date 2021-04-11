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
    public class ActorRepository : IActorRepository
    {
        private readonly CinemaDbContext _cinemaDbContext;
        public void AddActor(ActorDTO actorDTO)
        {
            _cinemaDbContext.Actors.Add(new CinemaActor
            {
                ActorName = actorDTO.ActorName
            });
            _cinemaDbContext.SaveChanges();
        }
        public void AddActorByMovieId(int id)
        {
            _cinemaDbContext.MovieActors.Add(new CinemaMovieActor { })
        }

    }
}
