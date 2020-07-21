using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyBookStore.Models;
using AutoMapper;
using MyBookStore.Dtos;

namespace MyBookStore.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private ConnectionClass _db;
        private readonly IMapper _mapper;

        public CustomersController(ConnectionClass db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        //Get api/customers
        [HttpGet]
        public IEnumerable<CustomerDto> GetCustomers()
        {
            var customer = _db.CustomersDb.ToList();
            var customerDtos = _mapper.Map<List<CustomerDto>>(customer);

            return customerDtos;
        }

        //Get api/customers/id
        [HttpGet("{Id}",Name = "GetCustomer")]
        public CustomerDto GetCustomer(int id)
        {
            var customer = _db.CustomersDb.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                Response.StatusCode = 404;

            return _mapper.Map<CustomerDto>(customer);
            

        }
        // POST api/customers

        [HttpPost]
        public IActionResult CreateCusomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
               return BadRequest();

            var customer = _mapper.Map<Customer>(customerDto);
            _db.CustomersDb.Add(customer);
            _db.SaveChanges();

            customerDto.Id = customer.Id;
            return Ok();
        }

        //Put api/customer/id

        [HttpPut]
        public void UpdateCustomer(int id, CustomerDto customer)
        {


            var customerInDb = _db.CustomersDb.SingleOrDefault(c => c.Id == id);

            // if (customer == null)
            //    throw Exception;

            _mapper.Map(customer, customerInDb);


            _db.SaveChanges();
          

        }

        //DELETE api/customer/id
        [HttpDelete]
        public IActionResult DeleteCustomer(int id)
        {
            var customerInDb = _db.CustomersDb.SingleOrDefault(c => c.Id == id);

            // if (customer == null)
            //    throw Exception;
            _db.CustomersDb.Remove(customerInDb);
            _db.SaveChanges();

            return Ok();
        }
    }
}