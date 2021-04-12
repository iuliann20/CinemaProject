using CinemaProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CinemaProject.Controllers
{
   public class HomeController : Controller
   {
      private readonly ILogger<HomeController> _logger;

      public HomeController(ILogger<HomeController> logger)
      {
         _logger = logger;
      }

      public IActionResult Index()
      {
         return View();
      }

      [HttpPost]
      public IActionResult Index(LocationChooserViewModel locationChooserViewModel)
      {
         CookieOptions option = new CookieOptions();
         option.Expires = DateTime.Now.AddDays(1);
         Response.Cookies.Append("CinemaLocation", locationChooserViewModel.SelectedLocation, option);
         return View();
      }

      public IActionResult LocationChooser()
      {
         LocationChooserViewModel vm = new LocationChooserViewModel {
            LocationNames = new List<string>
            {
                "Brasov","Bucuresti","Cluj","Baia Mare","Iasi"
            }
         };
         return View(vm);
      }
      public IActionResult Privacy()
      {
         return View();
      }

      [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
      public IActionResult Error()
      {
         return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
      }
   }
}
