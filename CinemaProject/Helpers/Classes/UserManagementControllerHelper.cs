using CinemaProject.Helpers.Interfaces;
using CinemaProject.Models;
using CinemaProject.TL.DTO;

namespace CinemaProject.Helpers.Classes
{
   public class UserManagementControllerHelper : IUserManagementControllerHelper
   {
      public UserViewModel BuildViewModel(CinemaUserDTO cinemaUserDTO)
      {
         return new UserViewModel {
            UserId = cinemaUserDTO.UserId,
            FirstName = cinemaUserDTO.FirstName,
            LastName = cinemaUserDTO.LastName,
            Email = cinemaUserDTO.Email,
            PhoneNumber = cinemaUserDTO.PhoneNumber,
            RoleName = cinemaUserDTO.RoleName,
         };
      }
      public CinemaUserDTO BuildDTO(UserViewModel movieViewModel)
      {
         return new CinemaUserDTO {
            UserId = movieViewModel.UserId,
            FirstName = movieViewModel.FirstName,
            LastName = movieViewModel.LastName,
            Email = movieViewModel.Email,
            PhoneNumber = movieViewModel.PhoneNumber,
            RoleName = movieViewModel.RoleName,
         };
      }
   }
}
