using CinemaProject.TL.DTO;
using CinemaProject.TL.Helpers;
using System.Collections.Generic;

namespace CinemaProject.BL.Interfaces
{
   public interface IUserLogic
   {
      Response AddUser(CinemaUserDTO registerDTO, string rePassword);
      CinemaUserDTO GetUserByEmail(string email);
      string GetFullName(int id);
      List<CinemaRoleDTO> GetRoleByUserId(int id);
      List<CinemaUserDTO> GetAllUsers();
      void DeleteUser(int id);
      List<string> GetAllRoles();
      bool EditUser(CinemaUserDTO cinemaUserDTO);
      CinemaUserDTO GetUserById(int userId);
   }
}
