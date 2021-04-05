using CinemaProject.BL.Interfaces;
using CinemaProject.DAL.Repository.Interfaces;
using CinemaProject.TL.DTO;
using CinemaProject.TL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaProject.BL.Classes
{
    public class UserLogic: IUserLogic
    {

        private readonly IUserRepository _userRepository;
        public UserLogic(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Response AddUser(CinemaUserDTO registerDTO, string rePassword)
        {
            if (_userRepository.GetUserByEmail(registerDTO.Email) != null)
            {
                return new Response
                {
                    IsCompletedSuccesfuly = false,
                    ResponseMessage = "A user with the same email already exists!"
                };
            }
            if (!registerDTO.Password.Equals(rePassword))
            {
                return new Response
                {
                    IsCompletedSuccesfuly = false,
                    ResponseMessage = "Passwords doesn't match!"
                };
            }
            _userRepository.AddUser(registerDTO);
            return new Response
            {
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

    }
}
