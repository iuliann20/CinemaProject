using CinemaProject.Models;
using CinemaProject.TL.DTO;

namespace CinemaProject.Helpers.Interfaces
{
   public interface IAccountControllerHelper
   {
      CinemaUserDTO BuildDTO(RegisterViewModel registerViewModel);
   }
}
