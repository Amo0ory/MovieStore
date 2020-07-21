using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyBookStore.Models
{
    public class Genres
    {
        [Key]
        public int GenreId { get; set; }
        public string Name { get; set; }
    }
}
