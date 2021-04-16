using CinemaProject.Models;
using CinemaProject.TL.DTO;
using System.Collections.Generic;

namespace CinemaProject.Helpers.Interfaces
{
   public interface IAccountControllerHelper
   {
      CinemaUserDTO BuildDTO(RegisterViewModel registerViewModel);
      CinemaUserDTO BuildDTO(UserViewModel userViewModel);
      List<string> VerifyViewModel(UserViewModel userViewModel);
      UserViewModel BuildViewModel(CinemaUserDTO userDto);
   }
}
