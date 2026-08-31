using Pharma.BLL.Services;
using Pharma.Dal.Repositories;
using Pharma.DAL.Context;
using Pharma.Entity.Entities;
using PharmacyMangementSystem.DTOs.Supplier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web.Http;

namespace PharmacyMangementSystem.Controllers
{
    [Authorize(Roles = "Admin, InventoryManager")]
    [RoutePrefix("api/suppliers")]
    public class SupplierController : ApiController
    {
        
        private readonly ISupplierService _service;

        public SupplierController(ISupplierService service)
        {
            _service= service;
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
                        GetCurrentEmployeeId()
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
                        GetCurrentEmployeeId()
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