using Pharma.BLL.Services;
using Pharma.DAL.Context;
using Pharma.Dal.Repositories;
using Pharma.Entity.Entities;
using PharmacyMangementSystem.DTOs.Supplier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace PharmacyMangementSystem.Controllers
{
    [RoutePrefix("api/suppliers")]
    public class SupplierController : ApiController
    {
        private readonly PharmacyDbContext _context;
        private readonly ISupplierService _service;

        // TEMPORARY until JWT/Auth is implemented
        private const int CurrentEmployeeId = 1;


        public SupplierController()
        {
            _context =
                new PharmacyDbContext();

            ISupplierRepository supplierRepository =
                new SupplierRepository(_context);

            _service =
                new SupplierService(
                    supplierRepository
                );
        }


        // GET: api/suppliers
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAllSuppliers()
        {
            var suppliers = _service
                .GetAllSuppliers()
                .Select(s => MapToDto(s))
                .ToList();

            return Ok(suppliers);
        }


        // GET: api/suppliers/1
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetSupplierById(
            int id)
        {
            try
            {
                var supplier =
                    _service.GetSupplierById(id);

                return Ok(
                    MapToDto(supplier)
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


        // POST: api/suppliers
        [HttpPost]
        [Route("")]
        public IHttpActionResult CreateSupplier(
            SupplierDto request)
        {
            if (request == null)
            {
                return BadRequest(
                    "Supplier data is required."
                );
            }

            try
            {
                var supplier =
                    new Supplier
                    {
                        SupplierName =
                            request.SupplierName,

                        ContactPersonName =
                            request.ContactPersonName,

                        ContactNumber =
                            request.ContactNumber,

                        Email =
                            request.Email,

                        Address =
                            request.Address
                    };


                var createdSupplier =
                    _service.CreateSupplier(
                        supplier,
                        CurrentEmployeeId
                    );


                return Content(
                    HttpStatusCode.Created,
                    MapToDto(createdSupplier)
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


        // PUT: api/suppliers/1
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult UpdateSupplier(
            int id,
            SupplierDto request)
        {
            if (request == null)
            {
                return BadRequest(
                    "Supplier data is required."
                );
            }

            try
            {
                var updatedSupplier =
                    new Supplier
                    {
                        SupplierName =
                            request.SupplierName,

                        ContactPersonName =
                            request.ContactPersonName,

                        ContactNumber =
                            request.ContactNumber,

                        Email =
                            request.Email,

                        Address =
                            request.Address,

                        IsActive =
                            request.IsActive
                    };


                var supplier =
                    _service.UpdateSupplier(
                        id,
                        updatedSupplier,
                        CurrentEmployeeId
                    );


                return Ok(
                    MapToDto(supplier)
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


        // Supplier Entity -> SupplierDto
        private SupplierDto MapToDto(
            Supplier supplier)
        {
            return new SupplierDto
            {
                SupplierId =
                    supplier.SupplierId,

                SupplierName =
                    supplier.SupplierName,

                ContactPersonName =
                    supplier.ContactPersonName,

                ContactNumber =
                    supplier.ContactNumber,

                Email =
                    supplier.Email,

                Address =
                    supplier.Address,

                IsActive =
                    supplier.IsActive,

                CreatedAt =
                    supplier.CreatedAt,

                UpdatedAt =
                    supplier.UpdatedAt
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