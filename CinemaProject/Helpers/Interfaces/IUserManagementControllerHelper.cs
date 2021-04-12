using CinemaProject.Models;
using CinemaProject.TL.DTO;

namespace CinemaProject.Helpers.Interfaces
{
   public interface IUserManagementControllerHelper
   {
      CinemaUserDTO BuildDTO(UserViewModel movieViewModel);
      UserViewModel BuildViewModel(CinemaUserDTO cinemaUserDTO);
   }
}
