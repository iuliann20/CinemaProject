using CinemaProject.Helpers.Interfaces;
using CinemaProject.Models;
using CinemaProject.TL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaProject.Helpers.Classes
{
    public class AccountControllerHelper: IAccountControllerHelper
    {
        public CinemaUserDTO BuildDTO(RegisterViewModel registerViewModel)
        {
            return new CinemaUserDTO
            {
                FirstName = registerViewModel.FirstName,
                LastName = registerViewModel.LastName,
                BirthDay = registerViewModel.BirthDay,
                PhoneNumber = registerViewModel.PhoneNumber,
                UserId = registerViewModel.UserId,
                Password = registerViewModel.Password,
                Email = registerViewModel.Email

            };
        }

    }
}
