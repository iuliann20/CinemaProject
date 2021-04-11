﻿using CinemaProject.DAL.Entities;
using CinemaProject.DAL.Repository.Interfaces;
using CinemaProject.TL.DTO;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CinemaProject.DAL.Repository.Classes
{
   public class MovieRepository : IMovieRepository
   {
      private readonly CinemaDbContext _cinemaDbContext;

      public MovieRepository(CinemaDbContext cinemaDbContext)
      {
         _cinemaDbContext = cinemaDbContext;
      }
      public List<MovieDTO> GetAllMovies()
      {
         List<MovieDTO> movies = _cinemaDbContext.CinemaMovies.Select(movie => new MovieDTO {
            MovieId = movie.MovieId,
            MovieName = movie.MovieName,
            Description = movie.Description,
            Duration = movie.Duration,
            MoviePhoto = movie.MoviePhoto,
         }).ToList();
         return movies;
      }
      public int AddMovie(MovieDTO movieDTO)
      {
         CinemaMovie movie = new CinemaMovie {
            MovieName = movieDTO.MovieName,
            Description = movieDTO.Description,
            Duration = movieDTO.Duration,
            MoviePhoto = movieDTO.MoviePhoto
         };
         _cinemaDbContext.CinemaMovies.Add(movie);
         _cinemaDbContext.SaveChanges();
         return movie.MovieId;
      }
      public MovieDTO GetMovieById(int id)
      {
         CinemaMovie movieById = _cinemaDbContext.CinemaMovies.FirstOrDefault(x => x.MovieId == id);
         return new MovieDTO {
            MovieId = movieById.MovieId,
            MovieName = movieById.MovieName,
            Description = movieById.Description,
            Duration = movieById.Duration,
            MoviePhoto = movieById.MoviePhoto,
            Actors = _cinemaDbContext.MovieActors
            .Include(x => x.CinemaActor)
            .Where(x => x.MovieId == movieById.MovieId)
            .Select(actor => actor.CinemaActor.ActorName)
            .ToList()
         };
      }
      public void RemoveMovie(int id)
      {
         CinemaMovie movieFromDb = _cinemaDbContext.CinemaMovies.FirstOrDefault(x => x.MovieId == id);
         if (movieFromDb != null) {
            _cinemaDbContext.Remove(movieFromDb);
            _cinemaDbContext.SaveChanges();
         }

      }

      public List<ReviewDTO> GetReviewsByMovieId(int movieId)
      {
         return _cinemaDbContext.CinemaReviews
            .Where(x => x.MovieId == movieId)
            .Select(review => new ReviewDTO {
               ReviewId = review.ReviewId,
               UserId = review.UserId,
               MovieId = review.MovieId,
               Review = review.Review,
               UserFirstName = _cinemaDbContext.Users.FirstOrDefault(x => x.UserId == review.UserId).FirstName
            }).ToList();
      }

      public void AddReview(ReviewDTO reviewDTO)
      {
         _cinemaDbContext.CinemaReviews.Add(new CinemaReview {
            MovieId = reviewDTO.MovieId,
            UserId = reviewDTO.UserId,
            Review = reviewDTO.Review
         });
         _cinemaDbContext.SaveChanges();
      }
   }
}
