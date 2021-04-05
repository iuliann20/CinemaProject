using CinemaProject.DAL.Entities;
using CinemaProject.DAL.Repository.Interfaces;
using CinemaProject.TL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaProject.DAL.Repository.Classes
{
    public class UserRepository: IUserRepository
    {
        private readonly CinemaDbContext _cinemaDbContext;

        public UserRepository(CinemaDbContext cinemaDbContext)
        {
            _cinemaDbContext = cinemaDbContext;
        }
        public void AddUser(CinemaUserDTO registerDTO)
        {
            _cinemaDbContext.Users.Add(new CinemaUser
            {
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName,
                BirthDay = registerDTO.BirthDay,
                Email = registerDTO.Email,
                PhoneNumber = registerDTO.PhoneNumber,
                Password = registerDTO.Password
            });
            _cinemaDbContext.SaveChanges();
        }
        public CinemaUserDTO GetUserByEmail(string email)
        {
            CinemaUser userFromDb = _cinemaDbContext.Users.FirstOrDefault(x => x.Email == email);
            if (userFromDb == null)
            {
                return null;
            }
            return new CinemaUserDTO
            {
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
            if (userFromDb == null)
            {
                return null;
            }
            return new CinemaUserDTO
            {
                UserId = userFromDb.UserId,
                FirstName = userFromDb.FirstName,
                LastName = userFromDb.LastName,
                BirthDay = userFromDb.BirthDay,
                Email = userFromDb.Email,
                PhoneNumber = userFromDb.PhoneNumber,
                Password = userFromDb.Password
            };
        }
    }
}
