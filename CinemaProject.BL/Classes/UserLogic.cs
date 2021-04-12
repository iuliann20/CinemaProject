﻿using CinemaProject.BL.Interfaces;
using CinemaProject.DAL.Repository.Interfaces;
using CinemaProject.TL.DTO;
using CinemaProject.TL.Helpers;
using System.Collections.Generic;

namespace CinemaProject.BL.Classes
{
   public class UserLogic : IUserLogic
   {

      private readonly IUserRepository _userRepository;
      public UserLogic(IUserRepository userRepository)
      {
         _userRepository = userRepository;
      }

      public Response AddUser(CinemaUserDTO registerDTO, string rePassword)
      {
         if (_userRepository.GetUserByEmail(registerDTO.Email) != null) {
            return new Response {
               IsCompletedSuccesfuly = false,
               ResponseMessage = "A user with the same email already exists!"
            };
         }
         if (!registerDTO.Password.Equals(rePassword)) {
            return new Response {
               IsCompletedSuccesfuly = false,
               ResponseMessage = "Passwords doesn't match!"
            };
         }
         var newUserId = _userRepository.AddUser(registerDTO);
         _userRepository.AddUserToRole(newUserId,_userRepository.GetRoleIdByName("User"));
         return new Response {
            IsCompletedSuccesfuly = true,
            ResponseMessage = "User added succesfuly!"
         };
      }
      public CinemaUserDTO GetUserByEmail(string email)
      {
         return _userRepository.GetUserByEmail(email);
      }
      public string GetFullName(int id)
      {
         CinemaUserDTO userDTO = _userRepository.GetUserById(id);
         return $"{userDTO.FirstName} {userDTO.LastName}";
      }
      public List<CinemaRoleDTO> GetRoleByUserId(int id)
      {
         return _userRepository.GetRoleByUserId(id);
      }
      public List<CinemaUserDTO> GetAllUsers()
      {
         return _userRepository.GetAllUsers();
      }

      public void DeleteUser(int id)
      {
         _userRepository.DeleteUser(id);
      }
   }
}
