using Pharma.BLL.Services;
using Pharma.Dal.Repositories;
using Pharma.DAL.Context;
using Pharma.Entity.Entities;
using PharmacyMangementSystem.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web.Http;

namespace PharmacyMangementSystem.Controllers
{
    [RoutePrefix("api/customers")]
    
    [Authorize(Roles = "Admin,Pharmacist")]
    public class CustomerController : ApiController
    {     
      
        private readonly ICustomerService _service;

   

        public CustomerController(ICustomerService service)
        {
            _service = service;
        }


        // GET: api/customers
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAllCustomers()
        {
            var customers = _service
                .GetAllCustomers()
                .Select(c => MapToDto(c))
                .ToList();

            return Ok(customers);
        }


        // GET: api/customers/1
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetCustomerById(
            int id)
        {
            try
            {
                var customer =
                    _service.GetCustomerById(id);

                return Ok(
                    MapToDto(customer)
                );
            }
            catch (ArgumentException ex)
            {
                return BadRequest(
                    ex.Message
                );
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }


        // POST: api/customers
        [HttpPost]
        [Route("")]
        public IHttpActionResult CreateCustomer(
            CustomerDto request)
        {
            if (request == null)
            {
                return BadRequest(
                    "Customer data is required."
                );
            }

            try
            {
                var customer =
                    new Customer
                    {
                        FirstName =
                            request.FirstName,

                        LastName =
                            request.LastName,

                        Email =
                            request.Email,

                        Contact =
                            request.Contact,

                        Address =
                            request.Address
                    };


                var createdCustomer =
                    _service.CreateCustomer(
                        customer,
                        GetCurrentEmployeeId()
                    );


                return Content(
                    HttpStatusCode.Created,
                    MapToDto(createdCustomer)
                );
            }
            catch (ArgumentException ex)
            {
                return BadRequest(
                    ex.Message
                );
            }
            catch (InvalidOperationException ex)
            {
                return Content(
                    HttpStatusCode.Conflict,
                    ex.Message
                );
            }
        }


        // PUT: api/customers/1
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult UpdateCustomer(
            int id,
            CustomerDto request)
        {
            if (request == null)
            {
                return BadRequest(
                    "Customer data is required."
                );
            }

            try
            {
                var updatedCustomer =
                    new Customer
                    {
                        FirstName =
                            request.FirstName,

                        LastName =
                            request.LastName,

                        Email =
                            request.Email,

                        Contact =
                            request.Contact,

                        Address =
                            request.Address
                    };


                var customer =
                    _service.UpdateCustomer(
                        id,
                        updatedCustomer,
                        GetCurrentEmployeeId()
                    );


                return Ok(
                    MapToDto(customer)
                );
            }
            catch (ArgumentException ex)
            {
                return BadRequest(
                    ex.Message
                );
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return Content(
                    HttpStatusCode.Conflict,
                    ex.Message
                );
            }
        }


        // Customer Entity -> CustomerDto
        private CustomerDto MapToDto(
            Customer customer)
        {
            return new CustomerDto
            {
                CustomerId =
                    customer.CustomerId,

                FirstName =
                    customer.FirstName,

                LastName =
                    customer.LastName,

                Email =
                    customer.Email,

                Contact =
                    customer.Contact,

                Address =
                    customer.Address
            };
        }


        private int GetCurrentEmployeeId()
        {
            var identity = User.Identity as ClaimsIdentity;

            var claim = identity?.FindFirst("EmployeeId");

            if (claim == null)
            {
                throw new UnauthorizedAccessException(
                    "Employee ID claim not found."
                );
            }

            return int.Parse(claim.Value);
        }
    }
}