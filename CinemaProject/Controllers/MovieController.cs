using CinemaProject.BL.Interfaces;
using CinemaProject.Helpers.Interfaces;
using CinemaProject.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaProject.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieLogic _movieLogic;
        private readonly IMovieControllerHelper _movieControllerHelper;

        public MovieController(IMovieLogic movieLogic, IMovieControllerHelper movieControllerHelper)
        {
            _movieLogic = movieLogic;
            _movieControllerHelper = movieControllerHelper;
        }

        public IActionResult Movies()
        {
            var listMovieViewModel = _movieControllerHelper.BuildListViewModel(_movieLogic.GelAllMovies());

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
            _movieLogic.AddMovie(_movieControllerHelper.BuildDTO(movieViewModel));
            return RedirectToAction("Movies");
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
