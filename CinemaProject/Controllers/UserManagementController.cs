using CinemaProject.BL.Interfaces;
using CinemaProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CinemaProject.Controllers
{
   public class UserManagementController : Controller
   {
      private readonly IUserLogic _userLogic;
      private readonly IAccountLogic _accountLogic;

      public UserManagementController(IUserLogic userLogic, IAccountLogic accountLogic)
      {
         _userLogic = userLogic;
         _accountLogic = accountLogic;
      }

      public IActionResult Index()
      {
         List<UserViewModel> users = _userLogic.GetAllUsers()
            .Where(x => x.UserId != _accountLogic.GetCurentUserId())
            .Select(userDTO => new UserViewModel {
               UserId = userDTO.UserId,
               FirstName = userDTO.FirstName,
               LastName = userDTO.LastName,
               Email = userDTO.Email,
               PhoneNumber = userDTO.PhoneNumber
            }).ToList();
         return View(users);
      }
   }
}
