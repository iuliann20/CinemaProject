using CinemaProject.DAL.Entities;
using CinemaProject.DAL.Repository.Interfaces;
using CinemaProject.TL.DTO;
using System.Linq;

namespace CinemaProject.DAL.Repository.Classes
{
   public class ActorRepository : IActorRepository
   {
      private readonly CinemaDbContext _cinemaDbContext;

      public ActorRepository(CinemaDbContext cinemaDbContext)
      {
         _cinemaDbContext = cinemaDbContext;
      }

      public int AddActor(ActorDTO actorDTO)
      {
         CinemaActor actor = new CinemaActor {
            ActorName = actorDTO.ActorName
         };
         _cinemaDbContext.Actors.Add(actor);
         _cinemaDbContext.SaveChanges();
         return actor.ActorId;
      }

      public void AddActorByMovieId(int actorId, int movieId)
      {
         _cinemaDbContext.MovieActors.Add(new CinemaMovieActor {
            ActorId = actorId,
            MovieId = movieId
         });
         _cinemaDbContext.SaveChanges();
      }

      public ActorDTO GetActorByName(string actorName)
      {
         CinemaActor actorByName = _cinemaDbContext.Actors.FirstOrDefault(x => x.ActorName == actorName);
         if (actorByName == null) {
            return null;
         }
         return new ActorDTO {
            ActorId = actorByName.ActorId,
            ActorName = actorByName.ActorName,
         };
      }
   }
}
