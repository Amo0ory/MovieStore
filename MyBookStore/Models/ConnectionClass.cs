using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyBookStore.ViewModels;

namespace MyBookStore.Models
{
    public class ConnectionClass: DbContext
    {

        public ConnectionClass(DbContextOptions<ConnectionClass>options):base(options)
        {

        }

        public DbSet<Customer> CustomersDb { get; set; }
        public DbSet<Movie> Movies { get; set; }

        public DbSet<MembershipType> MembershipType { get; set; }

        public DbSet<Genres> Genres { get; set; }
    }
}
