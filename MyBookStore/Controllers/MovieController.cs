using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyBookStore.Models;
using MyBookStore.ViewModels;
using Microsoft.EntityFrameworkCore;

// Save(var Movie) -> [HttpPost]
// Edit()
// 
namespace MyBookStore.Controllers
{
    public class MovieController : Controller
    {
        private readonly ConnectionClass _db;

        public MovieController(ConnectionClass db)
        {
           _db = db;
        }
        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
        }

        public IActionResult Index()
        {
            var movie = _db.Movies.ToList();
            return View(movie);
        }

        public IActionResult MovieDetails(int id)
        {
            var movie = _db.Movies.SingleOrDefault(m => m.Id == id);

            return View(movie);
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(Movie movie)
        {
            var genres = _db.Genres.ToList();
            
            if (movie.Id == 0)
            {
                movie.Id = _db.Movies.Count() +  1;
                movie.DateAdded = DateTime.Now;
             //   movie.Genre = _db.Genres.SingleOrDefault(m => m.GenreId == movie.GenreId);
                _db.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _db.Movies.SingleOrDefault(m => m.Id == movie.Id);
                movieInDb.Id = movie.Id;
                movieInDb.Name = movie.Name;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.DateAdded = movie.DateAdded;
                movieInDb.Genre = movie.Genre;
                movieInDb.NumberInStock = movie.NumberInStock;
                
            }
            _db.SaveChanges();
          

            return RedirectToAction("Index", "Movie");
        }
        public IActionResult New()
        {
            var genres = _db.Genres.ToList();

            

            var viewModel = new NewMovieForm
            {
                Genres = genres
            };
            
            return View("NewMovie",viewModel);
        }

        public IActionResult Edit(int id)
        {
            int d = id;
           // var movie = _db.Movies.SingleOrDefault(m => m.Id==id);
            var movie = _db.Movies.SingleOrDefault(m => m.Id == id);

            var viewModel = new NewMovieForm
            {
                Genres = _db.Genres.ToList(),
                Movie = movie
                
            };
            return View("NewMovie",viewModel);
        }

    }
}