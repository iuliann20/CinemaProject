using CinemaProject.Helpers.Interfaces;
using CinemaProject.Models;
using CinemaProject.TL.DTO;
using System.Collections.Generic;

namespace CinemaProject.Helpers.Classes
{
   public class AccountControllerHelper : IAccountControllerHelper
   {
      public CinemaUserDTO BuildDTO(RegisterViewModel registerViewModel)
      {
         return new CinemaUserDTO {
            FirstName = registerViewModel.FirstName,
            LastName = registerViewModel.LastName,
            BirthDay = registerViewModel.BirthDay,
            PhoneNumber = registerViewModel.PhoneNumber,
            UserId = registerViewModel.UserId,
            Password = registerViewModel.Password,
            Email = registerViewModel.Email
         };
      }

      public CinemaUserDTO BuildDTO(UserViewModel userViewModel)
      {
         return new CinemaUserDTO {
            FirstName = userViewModel.FirstName,
            LastName = userViewModel.LastName,
            PhoneNumber = userViewModel.PhoneNumber,
            UserId = userViewModel.UserId,
            Email = userViewModel.Email
         };
      }

      public UserViewModel BuildViewModel(CinemaUserDTO userDto)
      {
         return new UserViewModel {
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            PhoneNumber = userDto.PhoneNumber,
            UserId = userDto.UserId,
            Email = userDto.Email
         };
      }

      public List<string> VerifyViewModel(UserViewModel userViewModel)
      {
         List<string> listOfErrors = new List<string>();
         if (string.IsNullOrWhiteSpace(userViewModel.FirstName)) {
            listOfErrors.Add("Enter a first name.");
         }
         if (string.IsNullOrWhiteSpace(userViewModel.LastName)) {
            listOfErrors.Add("Enter a last name.");
         }
         if (string.IsNullOrWhiteSpace(userViewModel.Email)) {
            listOfErrors.Add("Enter a email.");
         }
         if (string.IsNullOrWhiteSpace(userViewModel.PhoneNumber)) {
            listOfErrors.Add("Enter a phone number.");
         }
         return listOfErrors;
      }
   }
}
