﻿using CinemaProject.BL.Interfaces;
using CinemaProject.Helpers.Interfaces;
using CinemaProject.Models;
using CinemaProject.TL.DTO;
using CinemaProject.TL.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CinemaProject.Controllers
{
   public class AccountController : Controller
   {
      private readonly IUserLogic _userLogic;
      private readonly IAccountControllerHelper _accountControllerHelper;
      private readonly IAccountLogic _accountLogic;
      public AccountController(IUserLogic userLogic, IAccountControllerHelper accountControllerHelper, IAccountLogic accountLogic)
      {
         _userLogic = userLogic;
         _accountControllerHelper = accountControllerHelper;
         _accountLogic = accountLogic;
      }
      [HttpGet]
      public IActionResult Register()
      {
         return View();
      }
      [HttpPost]
      public IActionResult Register(RegisterViewModel registerViewModel)
      {
         if (registerViewModel != null) {
            registerViewModel.Password = _accountLogic.EncryptPassword(registerViewModel.Password);
            registerViewModel.RePassword = _accountLogic.EncryptPassword(registerViewModel.RePassword);
            Response message = _userLogic.AddUser(_accountControllerHelper.BuildDTO(registerViewModel), registerViewModel.RePassword);
         }
         return RedirectToAction("Index", "Home");
      }
      [HttpPost]
      public IActionResult Login([FromBody] LoginViewModel loginViewModel)
      {
         if (string.IsNullOrEmpty(loginViewModel.Email) && string.IsNullOrEmpty(loginViewModel.Password)) {
            return Unauthorized();
         }
         CinemaUserDTO userByEmail = _userLogic.GetUserByEmail(loginViewModel.Email);
         if (userByEmail != null && userByEmail.Password.Equals(_accountLogic.EncryptPassword(loginViewModel.Password))) {
            int minutesToExpire = loginViewModel.RememberMe ? 1440 : 60;
            DateTime expirationDate = DateTime.Now.AddMinutes(minutesToExpire);
            Guid token = Guid.NewGuid();
            _accountLogic.AddToken(new TokenDTO {
               AccessToken = token.ToString(),
               ExpirationDate = expirationDate,
               UserId = userByEmail.UserId
            });
            HttpContext.Response.Cookies.Append("AuthenticationToken", token.ToString(), new CookieOptions { Expires = expirationDate });
            return Ok();
         }

         return NotFound();
      }

      public IActionResult Logout()
      {
         _accountLogic.Logout();
         return RedirectToAction("Index", "Home");
      }
   }
}
