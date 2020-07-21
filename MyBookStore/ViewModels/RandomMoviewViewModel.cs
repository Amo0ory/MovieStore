using MyBookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBookStore.ViewModels
{
    public class RandomMoviewViewModel
    {

        public Movie Movie { get; set; }
        public List<Customer> Customers { get; set; }

    }
}
