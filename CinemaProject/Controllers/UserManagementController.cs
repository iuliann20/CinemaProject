using CinemaProject.BL.Interfaces;
using CinemaProject.Helpers.Interfaces;
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
      private readonly IUserManagementControllerHelper _userManagementControllerHelper;

      public UserManagementController(IUserLogic userLogic, IAccountLogic accountLogic, IUserManagementControllerHelper userManagementControllerHelper)
      {
         _userLogic = userLogic;
         _accountLogic = accountLogic;
         _userManagementControllerHelper = userManagementControllerHelper;
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
               PhoneNumber = userDTO.PhoneNumber,
               RoleName = _userLogic.GetRoleByUserId(userDTO.UserId).FirstOrDefault().Role
            }).ToList();
         return View(users);
      }
      public IActionResult Delete(int id)
      {
         if (id != 0) {
            _userLogic.DeleteUser(id);
         }
         return RedirectToAction("Index");
      }
      public List<string> GetRoles()
      {
         return _userLogic.GetAllRoles();
      }

      [HttpPost]
      public bool EditUser([FromBody] UserViewModel userViewModel)
      {
         if (userViewModel != null) {
            return _userLogic.EditUser(_userManagementControllerHelper.BuildDTO(userViewModel));
         }
         return false;
      }
   }
}
