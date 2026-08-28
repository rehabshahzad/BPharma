using Pharma.BLL.Services;
using Pharma.Dal.Repositories;
using Pharma.DAL.Context;
using Pharma.Entity.Entities;
using PharmacyMangementSystem.DTOs.SupplierItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web.Http;

namespace PharmacyMangementSystem.Controllers
{
    [Authorize(Roles = "Admin,InventoryManager")]
    [RoutePrefix("api/supplier-items")]
    public class SupplierItemController : ApiController
    {
     
        private readonly ISupplierItemService _service;

   
        public SupplierItemController(ISupplierItemService service)
        {
            _service = service;
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
                        GetCurrentEmployeeId()
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
                        GetCurrentEmployeeId()
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