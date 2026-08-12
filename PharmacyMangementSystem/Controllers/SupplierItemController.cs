using Pharma.BLL.Services;
using Pharma.DAL.Context;
using Pharma.Dal.Repositories;
using Pharma.Entity.Entities;
using PharmacyMangementSystem.DTOs.SupplierItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace PharmacyMangementSystem.Controllers
{
    [RoutePrefix("api/supplier-items")]
    public class SupplierItemController : ApiController
    {
        private readonly PharmacyDbContext _context;
        private readonly ISupplierItemService _service;

        // TEMPORARY until JWT/Auth is implemented
        private const int CurrentEmployeeId = 1;


        public SupplierItemController()
        {
            _context =
                new PharmacyDbContext();

            ISupplierItemRepository repository =
                new SupplierItemRepository(
                    _context
                );

            _service =
                new SupplierItemService(
                    repository
                );
        }


        // GET: api/supplier-items
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAllSupplierItems()
        {
            var supplierItems =
                _service
                    .GetAllSupplierItems()
                    .Select(si => MapToDto(si))
                    .ToList();

            return Ok(supplierItems);
        }


        // GET: api/supplier-items/1
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetSupplierItemById(
            int id)
        {
            try
            {
                var supplierItem =
                    _service.GetSupplierItemById(id);

                return Ok(
                    MapToDto(supplierItem)
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


        // POST: api/supplier-items
        [HttpPost]
        [Route("")]
        public IHttpActionResult CreateSupplierItem(
            SupplierItemDto request)
        {
            if (request == null)
            {
                return BadRequest(
                    "Supplier item data is required."
                );
            }

            try
            {
                var supplierItem =
                    new SupplierItem
                    {
                        SupplierId =
                            request.SupplierId,

                        ItemId =
                            request.ItemId,

                        SupplierPrice =
                            request.SupplierPrice
                    };


                var created =
                    _service.CreateSupplierItem(
                        supplierItem,
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


        // PUT: api/supplier-items/1
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult UpdateSupplierItem(
            int id,
            SupplierItemDto request)
        {
            if (request == null)
            {
                return BadRequest(
                    "Supplier item data is required."
                );
            }

            try
            {
                var supplierItem =
                    new SupplierItem
                    {
                        SupplierId =
                            request.SupplierId,

                        ItemId =
                            request.ItemId,

                        SupplierPrice =
                            request.SupplierPrice,

                        IsActive =
                            request.IsActive
                    };


                var updated =
                    _service.UpdateSupplierItem(
                        id,
                        supplierItem,
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
            catch (InvalidOperationException ex)
            {
                return Content(
                    HttpStatusCode.Conflict,
                    ex.Message
                );
            }
        }


        // SupplierItem Entity -> SupplierItemDto
        private SupplierItemDto MapToDto(
            SupplierItem supplierItem)
        {
            return new SupplierItemDto
            {
                SupplierItemId =
                    supplierItem.SupplierItemId,

                SupplierId =
                    supplierItem.SupplierId,

                ItemId =
                    supplierItem.ItemId,

                SupplierPrice =
                    supplierItem.SupplierPrice,

                IsActive =
                    supplierItem.IsActive,

                CreatedAt =
                    supplierItem.CreatedAt,

                UpdatedAt =
                    supplierItem.UpdatedAt
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