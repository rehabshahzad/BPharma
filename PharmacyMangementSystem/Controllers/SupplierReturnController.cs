using Pharma.BLL.Services;
using Pharma.DAL.Context;
using Pharma.Dal.Repositories;
using Pharma.Entity.Entities;
using PharmacyMangementSystem.DTOs.SupplierReturn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace PharmacyMangementSystem.Controllers
{
    [RoutePrefix("api/supplier-returns")]
    public class SupplierReturnController : ApiController
    {
        private readonly PharmacyDbContext _context;
        private readonly ISupplierReturnService _service;

        private const int CurrentEmployeeId = 1;


        public SupplierReturnController()
        {
            _context =
                new PharmacyDbContext();

            ISupplierReturnRepository repository =
                new SupplierReturnRepository(
                    _context
                );

            _service =
                new SupplierReturnService(
                    repository
                );
        }


        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            var returns =
                _service
                    .GetAllSupplierReturns()
                    .Select(r => MapToDto(r))
                    .ToList();

            return Ok(returns);
        }


        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            try
            {
                return Ok(
                    MapToDto(
                        _service
                            .GetSupplierReturnById(id)
                    )
                );
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }


        [HttpPost]
        [Route("")]
        public IHttpActionResult Create(
            SupplierReturnDto request)
        {
            if (request == null)
            {
                return BadRequest(
                    "Supplier return data is required."
                );
            }

            try
            {
                var supplierReturn =
                    new SupplierReturn
                    {
                        PurchaseId =
                            request.PurchaseId,

                        Reason =
                            request.Reason,

                        Status =
                            request.Status
                    };


                var items =
                    request.Items?
                        .Select(i =>
                            new SupplierReturnItem
                            {
                                BatchId =
                                    i.BatchId,

                                ReturnQuantity =
                                    i.ReturnQuantity,

                                ReturnAmount =
                                    i.ReturnAmount,

                                Reason =
                                    i.Reason
                            })
                        .ToList();


                var created =
                    _service.CreateSupplierReturn(
                        supplierReturn,
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
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Content(
                    HttpStatusCode.Conflict,
                    ex.Message
                );
            }
        }


        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Update(
            int id,
            SupplierReturnDto request)
        {
            if (request == null)
            {
                return BadRequest(
                    "Supplier return data is required."
                );
            }

            try
            {
                var supplierReturn =
                    new SupplierReturn
                    {
                        Reason =
                            request.Reason,

                        Status =
                            request.Status
                    };


                var updated =
                    _service.UpdateSupplierReturn(
                        id,
                        supplierReturn,
                        CurrentEmployeeId
                    );


                return Ok(
                    MapToDto(updated)
                );
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }


        private SupplierReturnDto MapToDto(
            SupplierReturn supplierReturn)
        {
            return new SupplierReturnDto
            {
                SupplierReturnId =
                    supplierReturn.SupplierReturnId,

                PurchaseId =
                    supplierReturn.PurchaseId,

                ReturnDate =
                    supplierReturn.ReturnDate,

                Reason =
                    supplierReturn.Reason,

                Status =
                    supplierReturn.Status,

                CreatedAt =
                    supplierReturn.CreatedAt,

                UpdatedAt =
                    supplierReturn.UpdatedAt,

                Items =
                    supplierReturn.SupplierReturnItems?
                        .Select(i =>
                            new SupplierReturnItemDto
                            {
                                SupplierReturnItemId =
                                    i.SupplierReturnItemId,

                                BatchId =
                                    i.BatchId,

                                ReturnQuantity =
                                    i.ReturnQuantity,

                                ReturnAmount =
                                    i.ReturnAmount,

                                Reason =
                                    i.Reason
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