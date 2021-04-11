using CinemaProject.TL.DTO;

namespace CinemaProject.DAL.Repository.Interfaces
{
   public interface IActorRepository
   {
      int AddActor(ActorDTO actorDTO);
      ActorDTO GetActorByName(string actorName);
      void AddActorByMovieId(int actorId, int movieId);
   }
}
