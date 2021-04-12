using CinemaProject.TL.DTO;
using System.Collections.Generic;

namespace CinemaProject.DAL.Repository.Interfaces
{
   public interface IUserRepository
   {
      void AddUser(CinemaUserDTO registerDTO);
      CinemaUserDTO GetUserByEmail(string email);
      CinemaUserDTO GetUserById(int id);
      List<CinemaRoleDTO> GetRoleByUserId(int id);
      List<CinemaUserDTO> GetAllUsers();
   }
}
