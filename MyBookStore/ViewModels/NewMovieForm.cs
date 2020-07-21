using MyBookStore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyBookStore.ViewModels
{
    public class NewMovieForm
    {
        public IEnumerable<Genres> Genres { get; set; }

        public Movie Movie { get; set; }
        //public int Id { get; set; }

       
        //public string Name { get; set; }

        
        //public string Genre { get; set; }

        //public byte GenreId { get; set; }


        //public string ReleaseDate { get; set; }

        //public DateTime? DateAdded { get; set; }

        //public byte NumberInStock { get; set; }


        //public NewMovieForm()
        //{
        //    Id = 0;
        //}
        //public NewMovieForm(Movie movie)
        //{
        //    Id = movie.Id;
        //    Name = movie.Name;
        //    Genre = movie.Genre;
        //    GenreId = movie.GenreId;
        //    ReleaseDate = movie.ReleaseDate;
        //    DateAdded = movie.DateAdded;
        //    NumberInStock = movie.NumberInStock;

        //}
    }
}
