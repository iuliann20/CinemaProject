using CinemaProject.BL.Interfaces;
using CinemaProject.Helpers.Interfaces;
using CinemaProject.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaProject.Controllers
{
   public class MovieController : Controller
   {
      private readonly IMovieLogic _movieLogic;
      private readonly IMovieControllerHelper _movieControllerHelper;
      private readonly IWebHostEnvironment _hostingEnvironment;

      public MovieController(IMovieLogic movieLogic, IMovieControllerHelper movieControllerHelper, IWebHostEnvironment hostingEnvironment)
      {
         _movieLogic = movieLogic;
         _movieControllerHelper = movieControllerHelper;
         _hostingEnvironment = hostingEnvironment;
      }

      public IActionResult Movies()
      {
         var listMovieViewModel = _movieControllerHelper.BuildListViewModel(_movieLogic.GetAllMovies());

         return View(listMovieViewModel);
      }

      [HttpGet]
      public IActionResult AddMovie()
      {
         return View();
      }
      [HttpPost]
      public IActionResult AddMovie(MovieViewModel movieViewModel)
      {
         var modelErrors = _movieControllerHelper.VerifyViewModel(movieViewModel);
         if (modelErrors.Any()) {
            foreach(var error in modelErrors) {
               ModelState.AddModelError(error, error);
            }
            return View(movieViewModel);
         } else {
            Path.Combine(_hostingEnvironment.WebRootPath, "Content/MoviePhotos");
            _movieLogic.AddMovie(_movieControllerHelper.BuildDTO(movieViewModel));
            _movieLogic.UploadMoviePhoto(Path.Combine(_hostingEnvironment.WebRootPath, "Content/MoviePhotos"),movieViewModel.Photo);
            return RedirectToAction("Movies");
         }
      }
      public IActionResult ShowMovie(int id)
      {
         return View(_movieControllerHelper.BuildViewModel(_movieLogic.GetMovieById(id)));
      }
      public IActionResult RemoveMovie(int id)
      {
         _movieLogic.RemoveMovie(id);
         return RedirectToAction("Movies");
      }
   }
}
