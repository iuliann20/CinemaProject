using CinemaProject.TL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaProject.DAL.Repository.Interfaces
{
    public interface IActorRepository
    {
        void AddActor(ActorDTO actorDTO);
    }
}
