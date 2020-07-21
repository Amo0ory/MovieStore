using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyBookStore.Models;

namespace MyBookStore.ViewModels
{
    public class NewCustomerViewModel
    {
        public IEnumerable<MembershipType> MemebershipTypes { get; set; }

        public byte Selected { get; set; }

        public Customer Customer { get; set; }
    }
}
