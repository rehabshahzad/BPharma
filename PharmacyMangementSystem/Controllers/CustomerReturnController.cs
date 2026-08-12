using Pharma.BLL.Services;
using Pharma.DAL.Context;
using Pharma.Dal.Repositories;
using Pharma.Entity.Entities;
using PharmacyMangementSystem.DTOs.CustomerReturn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace PharmacyMangementSystem.Controllers
{
    [RoutePrefix("api/customer-returns")]
    public class CustomerReturnController
        : ApiController
    {
        private readonly PharmacyDbContext _context;
        private readonly ICustomerReturnService _service;

        // TEMPORARY until JWT/Auth
        private const int CurrentEmployeeId = 1;


        public CustomerReturnController()
        {
            _context =
                new PharmacyDbContext();

            ICustomerReturnRepository repository =
                new CustomerReturnRepository(
                    _context
                );

            _service =
                new CustomerReturnService(
                    repository
                );
        }


        // GET: api/customer-returns
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            var returns =
                _service
                    .GetAllCustomerReturns()
                    .Select(r => MapToDto(r))
                    .ToList();

            return Ok(returns);
        }


        // GET: api/customer-returns/1
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetById(
            int id)
        {
            try
            {
                var customerReturn =
                    _service
                        .GetCustomerReturnById(id);


                return Ok(
                    MapToDto(customerReturn)
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


        // POST: api/customer-returns
        [HttpPost]
        [Route("")]
        public IHttpActionResult Create(
            CustomerReturnDto request)
        {
            if (request == null)
            {
                return BadRequest(
                    "Customer return data is required."
                );
            }


            try
            {
                var customerReturn =
                    new CustomerReturn
                    {
                        SaleId =
                            request.SaleId,

                        Remarks =
                            request.Remarks,

                        Status =
                            request.Status
                    };


                var items =
                    request.Items?
                        .Select(i =>
                            new CustomerReturnItem
                            {
                                SaleItemId =
                                    i.SaleItemId,

                                BatchId =
                                    i.BatchId,

                                ReturnQuantity =
                                    i.ReturnQuantity,

                                // RefundAmount is NOT accepted
                                // from the request.

                                Reason =
                                    i.Reason,

                                CanReturnToStock =
                                    i.CanReturnToStock
                            })
                        .ToList();


                var created =
                    _service.CreateCustomerReturn(
                        customerReturn,
                        items,
                        CurrentEmployeeId
                    );


                return Content(
                    HttpStatusCode.Created,
                    MapToDto(created)
                );
            }
            catch (ArgumentException ex)
            {
                return BadRequest(
                    ex.Message
                );
            }
            catch (KeyNotFoundException ex)
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


        // PUT: api/customer-returns/1
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Update(
            int id,
            CustomerReturnDto request)
        {
            if (request == null)
            {
                return BadRequest(
                    "Customer return data is required."
                );
            }


            try
            {
                var customerReturn =
                    new CustomerReturn
                    {
                        Remarks =
                            request.Remarks,

                        Status =
                            request.Status
                    };


                var updated =
                    _service.UpdateCustomerReturn(
                        id,
                        customerReturn,
                        CurrentEmployeeId
                    );


                return Ok(
                    MapToDto(updated)
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


        // Entity -> DTO
        private CustomerReturnDto MapToDto(
            CustomerReturn customerReturn)
        {
            return new CustomerReturnDto
            {
                CustomerReturnId =
                    customerReturn.CustomerReturnId,

                SaleId =
                    customerReturn.SaleId,

                ReturnDate =
                    customerReturn.ReturnDate,

                Remarks =
                    customerReturn.Remarks,

                Status =
                    customerReturn.Status,

                CreatedAt =
                    customerReturn.CreatedAt,

                UpdatedAt =
                    customerReturn.UpdatedAt,

                Items =
                    customerReturn.CustomerReturnItems?
                        .Select(i =>
                            new CustomerReturnItemDto
                            {
                                CustomerReturnItemId =
                                    i.CustomerReturnItemId,

                                SaleItemId =
                                    i.SaleItemId,

                                BatchId =
                                    i.BatchId,

                                ReturnQuantity =
                                    i.ReturnQuantity,

                                RefundAmount =
                                    i.RefundAmount,

                                Reason =
                                    i.Reason,

                                CanReturnToStock =
                                    i.CanReturnToStock
                            })
                        .ToList()
            };
        }


        protected override void Dispose(
            bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}