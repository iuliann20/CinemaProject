using CinemaProject.Models;
using CinemaProject.TL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaProject.Helpers.Interfaces
{
   public interface IUserManagementControllerHelper
   {
      CinemaUserDTO BuildDTO(UserViewModel movieViewModel);
      UserViewModel BuildViewModel(CinemaUserDTO cinemaUserDTO);
   }
}
