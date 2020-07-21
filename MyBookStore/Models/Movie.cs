using MyBookStore.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyBookStore.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        
        [Required]
        public string Genre { get; set; }

        public byte GenreId { get; set; }

        [Required]
        public string ReleaseDate { get; set; }

       
        public DateTime? DateAdded { get; set; }
        [Required]
        public byte NumberInStock { get; set; }
    }
}
