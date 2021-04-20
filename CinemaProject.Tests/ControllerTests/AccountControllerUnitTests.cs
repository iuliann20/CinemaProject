using CinemaProject.BL.Interfaces;
using CinemaProject.Controllers;
using CinemaProject.Helpers.Interfaces;
using CinemaProject.Models;
using CinemaProject.Tests.Helpers;
using CinemaProject.TL.DTO;
using CinemaProject.TL.Helpers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CinemaProject.Tests.ControllerTests
{
   internal class AccountControllerUnitTests
   {
      private Mock<IUserLogic> _userLogic;
      private Mock<IAccountControllerHelper> _accountControllerHelper;
      private Mock<IAccountLogic> _accountLogic;
      private AccountController _accountController;

      [SetUp]
      public void Setup()
      {
         _userLogic = new Mock<IUserLogic>();
         _accountControllerHelper = new Mock<IAccountControllerHelper>();
         _accountLogic = new Mock<IAccountLogic>();

         _accountController = new AccountController(_userLogic.Object, _accountControllerHelper.Object, _accountLogic.Object);
      }

      [Test]
      public void Register_Get_Test()
      {
         ViewResult result = _accountController.Register() as ViewResult;

         Assert.That(result.ViewName, Is.EqualTo("Register"));
      }

      [Test]
      public void Register_Post_NullViewModel()
      {
         RedirectToActionResult result = _accountController.Register(null) as RedirectToActionResult;

         Assert.That(result.ControllerName, Is.EqualTo("Home"));
         Assert.That(result.ActionName, Is.EqualTo("Index"));
      }

      [Test]
      public void Register_Post_EmptyPassword()
      {
         ViewResult result = _accountController.Register(new RegisterViewModel { Password = "" }) as ViewResult;

         Assert.That(result.ViewName, Is.EqualTo("Register"));
         Assert.That(result.Model, Is.Not.Null);
         Assert.That(result.ViewData.ModelState.Count, Is.EqualTo(1));
         Assert.That(result.ViewData.ModelState["Password cannot be null"].Errors.FirstOrDefault().ErrorMessage, Is.Not.Null);
         Assert.That(result.ViewData.ModelState["Password cannot be null"].Errors.FirstOrDefault().ErrorMessage, Is.EqualTo("Password cannot be null"));
      }

      [Test]
      public void Register_Post_EmptyRePassword()
      {
         ViewResult result = _accountController.Register(new RegisterViewModel { RePassword = "" }) as ViewResult;

         Assert.That(result.ViewName, Is.EqualTo("Register"));
         Assert.That(result.Model, Is.Not.Null);
         Assert.That(result.ViewData.ModelState.Count, Is.EqualTo(1));
         Assert.That(result.ViewData.ModelState["Password cannot be null"].Errors.FirstOrDefault().ErrorMessage, Is.Not.Null);
         Assert.That(result.ViewData.ModelState["Password cannot be null"].Errors.FirstOrDefault().ErrorMessage, Is.EqualTo("Password cannot be null"));
      }

      [Test]
      public void Register_Unsuccessfully()
      {
         _accountLogic.Setup(x => x.EncryptPassword(It.IsAny<string>())).Returns("parolaencryptata").Verifiable();
         _accountControllerHelper.Setup(x => x.BuildDTO(It.IsAny<RegisterViewModel>())).Returns(new CinemaUserDTO()).Verifiable();
         string errorMessage = "Nu a mers";
         _userLogic.Setup(x => x.AddUser(It.IsAny<CinemaUserDTO>(), It.IsAny<string>())).Returns(new Response { IsCompletedSuccesfuly = false, ResponseMessage = errorMessage }).Verifiable();

         ViewResult result = _accountController.Register(new RegisterViewModel { Password = "parola", RePassword = "parola" }) as ViewResult;

         Assert.That(result.ViewName, Is.EqualTo("Register"));
         Assert.That(result.Model, Is.Not.Null);
         Assert.That(result.ViewData.ModelState.Count, Is.EqualTo(1));
         Assert.That(result.ViewData.ModelState[errorMessage].Errors.FirstOrDefault().ErrorMessage, Is.Not.Null);
         Assert.That(result.ViewData.ModelState[errorMessage].Errors.FirstOrDefault().ErrorMessage, Is.EqualTo(errorMessage));

         _accountLogic.Verify(x => x.EncryptPassword(It.IsAny<string>()), Times.Exactly(2));
         _accountControllerHelper.Verify(x => x.BuildDTO(It.IsAny<RegisterViewModel>()), Times.Exactly(1));
         _userLogic.Verify(x => x.AddUser(It.IsAny<CinemaUserDTO>(), It.IsAny<string>()), Times.Exactly(1));

      }

      [Test]
      public void Register_Successfully()
      {
         _accountLogic.Setup(x => x.EncryptPassword(It.IsAny<string>())).Returns("parolaencryptata").Verifiable();
         _accountControllerHelper.Setup(x => x.BuildDTO(It.IsAny<RegisterViewModel>())).Returns(new CinemaUserDTO()).Verifiable();
         _userLogic.Setup(x => x.AddUser(It.IsAny<CinemaUserDTO>(), It.IsAny<string>())).Returns(new Response { IsCompletedSuccesfuly = true }).Verifiable();

         RedirectToActionResult result = _accountController.Register(new RegisterViewModel { Password = "parola", RePassword = "parola" }) as RedirectToActionResult;

         Assert.That(result.ControllerName, Is.EqualTo("Home"));
         Assert.That(result.ActionName, Is.EqualTo("Index"));

         _accountLogic.Verify(x => x.EncryptPassword(It.IsAny<string>()), Times.Exactly(2));
         _accountControllerHelper.Verify(x => x.BuildDTO(It.IsAny<RegisterViewModel>()), Times.Exactly(1));
         _userLogic.Verify(x => x.AddUser(It.IsAny<CinemaUserDTO>(), It.IsAny<string>()), Times.Exactly(1));
      }

      [Test]
      public void Login_Unauthorized()
      {
         StatusCodeResult result = _accountController.Login(new LoginViewModel { Email = "", Password = "" }) as StatusCodeResult;

         Assert.That(result.StatusCode, Is.EqualTo(401));
      }

      [Test]
      public void Login_UserNotFound()
      {
         _userLogic.Setup(x => x.GetUserByEmail(It.IsAny<string>())).Returns(null as CinemaUserDTO).Verifiable();

         StatusCodeResult result = _accountController.Login(new LoginViewModel { Email = "email", Password = "password" }) as StatusCodeResult;

         Assert.That(result.StatusCode, Is.EqualTo(404));

         _userLogic.Verify(x => x.GetUserByEmail(It.IsAny<string>()), Times.Exactly(1));
      }

      [Test]
      public void Login_Successful()
      {
         _userLogic.Setup(x => x.GetUserByEmail(It.IsAny<string>())).Returns(new CinemaUserDTO { Email = "email", Password = "password" }).Verifiable();
         _accountLogic.Setup(x => x.EncryptPassword(It.IsAny<string>())).Returns("password").Verifiable();
         _accountController.ControllerContext = new ControllerContext {
            HttpContext = ReturnValuesHelper.GetMockHttpContext()
         };

         StatusCodeResult result = _accountController.Login(new LoginViewModel { Email = "email", Password = "password" }) as StatusCodeResult;

         Assert.That(result.StatusCode, Is.EqualTo(200));

         _userLogic.Verify(x => x.GetUserByEmail(It.IsAny<string>()), Times.Exactly(1));
         _accountLogic.Verify(x => x.EncryptPassword(It.IsAny<string>()), Times.Exactly(1));
      }

      [Test]
      public void Logout_Successful()
      {
         RedirectToActionResult result = _accountController.Logout() as RedirectToActionResult;

         Assert.That(result.ControllerName, Is.EqualTo("Home"));
         Assert.That(result.ActionName, Is.EqualTo("Index"));
      }

      [Test]
      public void EditProfile_Get()
      {
         _userLogic.Setup(x => x.GetUserById(It.IsAny<int>())).Returns(new CinemaUserDTO { Email = "email", Password = "password" }).Verifiable();
         _accountLogic.Setup(x => x.GetCurentUserId()).Returns(1).Verifiable();
         _accountControllerHelper.Setup(x => x.BuildViewModel(It.IsAny<CinemaUserDTO>())).Returns(new UserViewModel { Email = "email", FirstName = "Iulian", LastName = "Silitra" }).Verifiable();

         ViewResult result = _accountController.EditProfile() as ViewResult;

         Assert.That(result.ViewName, Is.EqualTo("EditProfile"));
         Assert.That(result.Model, Is.Not.Null);

         _userLogic.Verify(x => x.GetUserById(It.IsAny<int>()), Times.Exactly(1));
         _accountLogic.Verify(x => x.GetCurentUserId(), Times.Exactly(1));
         _accountControllerHelper.Verify(x => x.BuildViewModel(It.IsAny<CinemaUserDTO>()), Times.Exactly(1));
      }

      [Test]
      public void EditProfile_Post_NullModel()
      {
         RedirectToActionResult result = _accountController.EditProfile(null) as RedirectToActionResult;

         Assert.That(result.ControllerName, Is.EqualTo("Home"));
         Assert.That(result.ActionName, Is.EqualTo("Index"));
      }

      [Test]
      public void EditProfile_Post_ModelStateError()
      {
         string errorMessage = "Enter a first name.";
         _accountControllerHelper.Setup(x => x.VerifyViewModel(It.IsAny<UserViewModel>())).Returns(new List<string> { errorMessage }).Verifiable();

         ViewResult result = _accountController.EditProfile(new UserViewModel()) as ViewResult;

         Assert.That(result.ViewName, Is.EqualTo("EditProfile"));
         Assert.That(result.Model, Is.Not.Null);
         Assert.That(result.ViewData.ModelState.Count, Is.EqualTo(1));
         Assert.That(result.ViewData.ModelState[errorMessage].Errors.FirstOrDefault().ErrorMessage, Is.Not.Null);
         Assert.That(result.ViewData.ModelState[errorMessage].Errors.FirstOrDefault().ErrorMessage, Is.EqualTo(errorMessage));

         _accountControllerHelper.Verify(x => x.VerifyViewModel(It.IsAny<UserViewModel>()), Times.Exactly(1));
      }

      [Test]
      public void EditProfile_Post_Succesful()
      {
         _accountControllerHelper.Setup(x => x.VerifyViewModel(It.IsAny<UserViewModel>())).Returns(new List<string>()).Verifiable();

         RedirectToActionResult result = _accountController.EditProfile(new UserViewModel()) as RedirectToActionResult;

         Assert.That(result.ControllerName, Is.EqualTo("Home"));
         Assert.That(result.ActionName, Is.EqualTo("Index"));

         _accountControllerHelper.Verify(x => x.VerifyViewModel(It.IsAny<UserViewModel>()), Times.Exactly(1));
      }
   }
}
