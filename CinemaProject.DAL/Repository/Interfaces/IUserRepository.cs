using CinemaProject.TL.DTO;
using System.Collections.Generic;

namespace CinemaProject.DAL.Repository.Interfaces
{
   public interface IUserRepository
   {
      int AddUser(CinemaUserDTO registerDTO);
      CinemaUserDTO GetUserByEmail(string email);
      CinemaUserDTO GetUserById(int id);
      List<CinemaRoleDTO> GetRoleByUserId(int id);
      List<CinemaUserDTO> GetAllUsers();
      void DeleteUser(int id);
      int GetRoleIdByName(string roleName);
      void AddUserToRole(int newUserId, int roleId);
      List<string> GetAllRoles();
      bool EditUser(CinemaUserDTO cinemaUserDTO);
      void ChangeUserRole(int userId, string roleName);
   }
}
