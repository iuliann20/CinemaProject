using Microsoft.AspNetCore.Mvc;

namespace CinemaProject.Controllers
{
   public class UserManagementController : Controller
   {
      public IActionResult Index()
      {
         return View();
      }
   }
}
