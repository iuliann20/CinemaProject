using CinemaProject.BL.Interfaces;
using CinemaProject.Helpers.Interfaces;
using CinemaProject.Models;
using CinemaProject.TL.DTO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CinemaProject.Controllers
{
   public class MovieController : Controller
   {
      private readonly IMovieLogic _movieLogic;
      private readonly IMovieControllerHelper _movieControllerHelper;
      private readonly IWebHostEnvironment _hostingEnvironment;
      private readonly IAccountLogic _accountLogic;

      public MovieController(IMovieLogic movieLogic, IMovieControllerHelper movieControllerHelper, IWebHostEnvironment hostingEnvironment, IAccountLogic accountLogic)
      {
         _movieLogic = movieLogic;
         _movieControllerHelper = movieControllerHelper;
         _hostingEnvironment = hostingEnvironment;
         _accountLogic = accountLogic;
      }

      public IActionResult Movies()
      {
         List<MovieViewModel> listMovieViewModel = _movieControllerHelper.BuildListViewModel(_movieLogic.GetAllMovies());

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
         List<string> modelErrors = _movieControllerHelper.VerifyViewModel(movieViewModel);
         if (modelErrors.Any()) {
            foreach (string error in modelErrors) {
               ModelState.AddModelError(error, error);
            }
            return View(movieViewModel);
         } else {
            Path.Combine(_hostingEnvironment.WebRootPath, "Content/MoviePhotos");
            _movieLogic.AddMovie(_movieControllerHelper.BuildDTO(movieViewModel));
            if (movieViewModel.Photo != null) {
               _movieLogic.UploadMoviePhoto(Path.Combine(_hostingEnvironment.WebRootPath, "Content/MoviePhotos"), movieViewModel.Photo);
            }
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

      public PartialViewResult GetReviewsByMovieId(int id)
      {
         List<ReviewViewModel> reviews = _movieLogic.GetReviewsByMovieId(id)
            .Select(reviewDTO => new ReviewViewModel {
               Review = reviewDTO.Review,
               ReviewId = reviewDTO.ReviewId,
               UserFirstName = reviewDTO.UserFirstName
            }).ToList();
         return PartialView("ReviewLayout", reviews);
      }

      [HttpPost]
      public bool AddReview([FromBody] ReviewViewModel reviewViewModel)
      {
         if (reviewViewModel != null) {
            return _movieLogic.AddReview(new ReviewDTO {
               MovieId = reviewViewModel.MovieId,
               Review = reviewViewModel.Review,
               UserId = _accountLogic.GetCurentUserId()
            });
         }
         return false;
      }

      [HttpPost]
      public bool RemoveReview(int id)
      {
         if (id != 0) {
            if (_accountLogic.IsAdmin() || _movieLogic.CanRemoveReview(_accountLogic.GetCurentUserId(), id)) {
               _movieLogic.RemoveReview(id);
               return true;
            } else {
               return false;
            }
         }
         return false;
      }

      public IActionResult ManageBroadcast(int id)
      {
         string locationName = HttpContext.Request.Cookies["CinemaLocation"];
         if (string.IsNullOrEmpty(locationName)) {
            return RedirectToAction("Movies");
         }
         List<CinemaBroadcastDTO> broadcasts = _movieLogic.GetBroadcastsByMovieIdAndLocationName(id, locationName);
         List<CinemaBroadcastViewModel> model = broadcasts.Select(b => new CinemaBroadcastViewModel {
            BroadcastId = b.BroadcastId,
            MovieId = b.MovieId,
            CinemaLocationId = b.CinemaLocationId,
            PriceId = b.PriceId,
            Time = b.Time,
            CinemaLocationDTO = b.CinemaLocationDTO,
            PriceDTO = b.PriceDTO,
         }).ToList();
         return View(model);
      }
   }
}
