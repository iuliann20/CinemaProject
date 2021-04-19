using CinemaProject.BL.Interfaces;
using CinemaProject.Helpers.Interfaces;
using CinemaProject.Models;
using CinemaProject.TL.DTO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
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
         List<CinemaBroadcastViewModel> broadcastsViewModel = broadcasts.Select(b => new CinemaBroadcastViewModel {
            BroadcastId = b.BroadcastId,
            MovieId = b.MovieId,
            CinemaLocationId = b.CinemaLocationId,
            PriceId = b.PriceId,
            Time = b.Time,
            CinemaLocationDTO = b.CinemaLocationDTO,
            PriceDTO = b.PriceDTO,
            NumberOfSeats = b.NumberOfSeats,
         }).ToList();
         AllBroadcastsViewModel model = new AllBroadcastsViewModel {
            MovieId = id,
            CinemaBroadcastViewModels = broadcastsViewModel
         };
         return View(model);
      }

      [HttpGet]
      public IActionResult AddBroadcast(int id)
      {
         string locationName = HttpContext.Request.Cookies["CinemaLocation"];
         if (string.IsNullOrEmpty(locationName)) {
            return RedirectToAction("Movies");
         }
         CinemaBroadcastViewModel model = new CinemaBroadcastViewModel {
            MovieId = id,
            CinemaLocationId = _movieLogic.GetLocationIdByName(locationName),
            Time = DateTime.Now
         };
         return View(model);
      }

      [HttpPost]
      public IActionResult AddBroadcast(CinemaBroadcastViewModel cinemaBroadcastViewModel)
      {
         _movieLogic.AddBroadcast(_movieControllerHelper.BuildDTO(cinemaBroadcastViewModel), cinemaBroadcastViewModel.Price);

         return RedirectToAction("Movies");
      }
      public IActionResult DeleteBroadcast(int id)
      {
         _movieLogic.DeleteBroadcast(id);
         return RedirectToAction("Movies");
      }

      public IActionResult ReserveTicket(int id)
      {
         string locationName = HttpContext.Request.Cookies["CinemaLocation"];
         if (string.IsNullOrEmpty(locationName)) {
            return RedirectToAction("Movies");
         }
         List<CinemaBroadcastViewModel> avaiableBroadcasts = _movieLogic.GetBroadcastsByMovieIdAndLocationName(id, locationName)
            .Select(b => new CinemaBroadcastViewModel {
               BroadcastId = b.BroadcastId,
               MovieId = b.MovieId,
               CinemaLocationId = b.CinemaLocationId,
               PriceId = b.PriceId,
               Time = b.Time,
               CinemaLocationDTO = b.CinemaLocationDTO,
               PriceDTO = b.PriceDTO,
               NumberOfSeats = b.NumberOfSeats,
            }).ToList();
         return View(avaiableBroadcasts);
      }

      [HttpGet]
      public IActionResult MakeReservation(int id, int numberOfSelectedSeats)
      {
         _movieLogic.MakeReservation(id, numberOfSelectedSeats, _accountLogic.GetCurentUserId());
         return RedirectToAction("Movies");
      }

      public IActionResult Reservations()
      {
         int userId = _accountLogic.GetCurentUserId();
         string locationName = HttpContext.Request.Cookies["CinemaLocation"];
         if (string.IsNullOrEmpty(locationName)) {
            return RedirectToAction("Index", "Home");
         }
         List<CinemaBookingViewModel> reservations = _movieLogic.GetBookingsByUserIdAndLocationName(userId, locationName)
            .Select(x => new CinemaBookingViewModel {
               BookingId = x.BookingId,
               BroadcastId = x.BroadcastId,
               UserId = x.UserId,
               MovieName = x.MovieName,
               Price = x.Price,
               AvalableSeats = x.AvalableSeats,
               Seat = x.Seat,
               CinemaName = x.CinemaName,
               Time = x.Time,
               MovieId = x.MovieId
            }).ToList();
         return View(reservations);
      }
      public IActionResult RemoveBooking(int id)
      {
         _movieLogic.DeleteBooking(id);
         return RedirectToAction("Reservations");
      }
   }
}

