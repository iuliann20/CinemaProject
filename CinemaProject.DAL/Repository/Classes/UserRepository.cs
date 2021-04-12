﻿using CinemaProject.DAL.Entities;
using CinemaProject.DAL.Repository.Interfaces;
using CinemaProject.TL.DTO;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CinemaProject.DAL.Repository.Classes
{
   public class UserRepository : IUserRepository
   {
      private readonly CinemaDbContext _cinemaDbContext;

      public UserRepository(CinemaDbContext cinemaDbContext)
      {
         _cinemaDbContext = cinemaDbContext;
      }
      public int AddUser(CinemaUserDTO registerDTO)
      {
         var newUser = new CinemaUser {
            FirstName = registerDTO.FirstName,
            LastName = registerDTO.LastName,
            BirthDay = registerDTO.BirthDay,
            Email = registerDTO.Email,
            PhoneNumber = registerDTO.PhoneNumber,
            Password = registerDTO.Password
         };
         _cinemaDbContext.Users.Add(newUser);
         _cinemaDbContext.SaveChanges();
         return newUser.UserId;
      }
      public CinemaUserDTO GetUserByEmail(string email)
      {
         CinemaUser userFromDb = _cinemaDbContext.Users.FirstOrDefault(x => x.Email == email);
         if (userFromDb == null) {
            return null;
         }
         return new CinemaUserDTO {
            UserId = userFromDb.UserId,
            FirstName = userFromDb.FirstName,
            LastName = userFromDb.LastName,
            BirthDay = userFromDb.BirthDay,
            Email = userFromDb.Email,
            PhoneNumber = userFromDb.PhoneNumber,
            Password = userFromDb.Password
         };
      }
      public CinemaUserDTO GetUserById(int id)
      {
         CinemaUser userFromDb = _cinemaDbContext.Users.FirstOrDefault(x => x.UserId == id);
         if (userFromDb == null) {
            return null;
         }
         return new CinemaUserDTO {
            UserId = userFromDb.UserId,
            FirstName = userFromDb.FirstName,
            LastName = userFromDb.LastName,
            BirthDay = userFromDb.BirthDay,
            Email = userFromDb.Email,
            PhoneNumber = userFromDb.PhoneNumber,
            Password = userFromDb.Password
         };
      }
      public List<CinemaRoleDTO> GetRoleByUserId(int id)
      {
         return _cinemaDbContext.CinemaUserRoles.Include(x => x.CinemaRole).Where(x => x.UserId == id).Select(role => new CinemaRoleDTO {
            RoleId = role.RoleId,
            Role = role.CinemaRole.Role
         }).ToList();
      }
      public List<CinemaUserDTO> GetAllUsers()
      {
         List<CinemaUserDTO> users = _cinemaDbContext.Users.Select(user => new CinemaUserDTO {
            UserId = user.UserId,
            FirstName = user.FirstName,
            LastName = user.LastName,
            BirthDay = user.BirthDay,
            Email = user.Email,
            Password = user.Password,
            PhoneNumber = user.PhoneNumber
         }).ToList();
         return users;
      }

      public void DeleteUser(int id)
      {
         var userFromDb = _cinemaDbContext.Users.FirstOrDefault(x => x.UserId == id);
         if (userFromDb != null) {
            _cinemaDbContext.Remove(userFromDb);
            _cinemaDbContext.SaveChanges();
         }
      }

      public int GetRoleIdByName(string roleName)
      {
         return _cinemaDbContext.CinemaRoles.FirstOrDefault(x => x.Role == roleName).RoleId;
      }

      public void AddUserToRole(int newUserId, int roleId)
      {
         _cinemaDbContext.CinemaUserRoles.Add(new CinemaUserRole {
            UserId = newUserId,
            RoleId = roleId
         });
      }
   }
}
