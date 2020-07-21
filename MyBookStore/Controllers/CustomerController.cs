using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Controller;
using MyBookStore.Models;
using MyBookStore.ViewModels;

namespace MyBookStore.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ConnectionClass _db;

        public CustomerController(ConnectionClass db)
        {
            _db = db;
        }
        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
        }
        [Authorize]
        [AllowAnonymous]
        public IActionResult Index()
        {
            var customers = _db.CustomersDb.Include(c => c.MembershipType).ToList();
            return View(customers);
        }
        public IActionResult Details(int id)
        {
            var customer = new Customer();
            customer = _db.CustomersDb.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);


            return View(customer);
        }
        public IActionResult New()
        {
            var membershiptype = _db.MembershipType.ToList();

            var viewModel = new NewCustomerViewModel
            {
                Customer = new Customer(),
                MemebershipTypes = membershiptype
            };
            return View("CustomerForm", viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(NewCustomerViewModel customerView)
        {
            //if (!ModelState.IsValid)
            //{
            //    //var viewModel = new NewCustomerViewModel
            //    //{
            //    //    Customer = customer,
            //    //    Selected = customer.MembershipTypeId,
            //    //    MemebershipTypes = _db.MembershipType.ToList()
            //    //};
            //    //return View("CustomerForm", viewModel);
            //}

            if (customerView.Customer.Id == 0)
                _db.CustomersDb.Add(customerView.Customer);
            else
            {
                var customerInDb = _db.CustomersDb.Single(c => c.Id == customerView.Customer.Id);

                customerInDb.Name = customerView.Customer.Name;
                customerInDb.IsSubscribedToNewsLetter = customerView.Customer.IsSubscribedToNewsLetter;
                customerInDb.MembershipType = customerView.Customer.MembershipType;
                customerInDb.MembershipTypeId = customerView.Selected;
                customerInDb.DOB = customerView.Customer.DOB;

            }
            _db.SaveChanges();
            return RedirectToAction("Index", "Customer");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {


            var customer = _db.CustomersDb.SingleOrDefault(c => c.Id == id);
            var membershiptype = _db.MembershipType.ToList();

            var Selected = customer.MembershipTypeId;
            if (customer == null)
                return NotFound();

            var viewModel = new NewCustomerViewModel
            {
                Customer = customer,
                Selected = Selected,
                MemebershipTypes = membershiptype
                
            };

            return View("CustomerForm",viewModel);
        }
        [HttpPost]
        public IActionResult DeleteCustomer(int id)
        {
            int d = id;
            var customer = _db.CustomersDb.SingleOrDefault(c => c.Id == id);
            _db.Remove(customer);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}