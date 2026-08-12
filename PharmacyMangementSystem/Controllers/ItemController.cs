using Pharma.BLL.Services;
using Pharma.DAL.Context;
using Pharma.Dal.Repositories;
using Pharma.Entity.Entities;
using PharmacyMangementSystem.DTOs.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace PharmacyMangementSystem.Controllers
{
    [RoutePrefix("api/items")]
    public class ItemController : ApiController
    {
        private readonly PharmacyDbContext _context;
        private readonly IItemService _service;

        // TEMPORARY until JWT/Auth
        private const int CurrentEmployeeId = 1;


        public ItemController()
        {
            _context =
                new PharmacyDbContext();

            IItemRepository itemRepository =
                new ItemRepository(_context);

            _service =
                new ItemService(itemRepository);
        }


        // GET: api/items
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAllItems()
        {
            var items =
                _service
                    .GetAllItems()
                    .Select(i => MapToDto(i))
                    .ToList();

            return Ok(items);
        }


        // GET: api/items/1
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetItemById(
            int id)
        {
            try
            {
                var item =
                    _service.GetItemById(id);

                return Ok(
                    MapToDto(item)
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


        // POST: api/items
        [HttpPost]
        [Route("")]
        public IHttpActionResult CreateItem(
            ItemDto request)
        {
            if (request == null)
            {
                return BadRequest(
                    "Item data is required."
                );
            }

            try
            {
                var item =
                    new Item
                    {
                        CategoryId =
                            request.CategoryId,

                        BrandId =
                            request.BrandId,

                        FormulaId =
                            request.FormulaId,

                        ItemName =
                            request.ItemName,

                        Description =
                            request.Description,

                        PictureUrl =
                            request.PictureUrl,

                        Barcode =
                            request.Barcode,

                        IsPrescriptionRequired =
                            request.IsPrescriptionRequired,

                        SellingPrice =
                            request.SellingPrice,

                        MinimumStockLevel =
                            request.MinimumStockLevel,

                        RackNumber =
                            request.RackNumber,

                        ShelfNumber =
                            request.ShelfNumber,

                        LaneNumber =
                            request.LaneNumber
                    };


                var createdItem =
                    _service.CreateItem(
                        item,
                        CurrentEmployeeId
                    );


                return Content(
                    HttpStatusCode.Created,
                    MapToDto(createdItem)
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


        // PUT: api/items/1
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult UpdateItem(
            int id,
            ItemDto request)
        {
            if (request == null)
            {
                return BadRequest(
                    "Item data is required."
                );
            }

            try
            {
                var item =
                    new Item
                    {
                        CategoryId =
                            request.CategoryId,

                        BrandId =
                            request.BrandId,

                        FormulaId =
                            request.FormulaId,

                        ItemName =
                            request.ItemName,

                        Description =
                            request.Description,

                        PictureUrl =
                            request.PictureUrl,

                        Barcode =
                            request.Barcode,

                        IsPrescriptionRequired =
                            request.IsPrescriptionRequired,

                        SellingPrice =
                            request.SellingPrice,

                        MinimumStockLevel =
                            request.MinimumStockLevel,

                        RackNumber =
                            request.RackNumber,

                        ShelfNumber =
                            request.ShelfNumber,

                        LaneNumber =
                            request.LaneNumber,

                        IsActive =
                            request.IsActive
                    };


                var updatedItem =
                    _service.UpdateItem(
                        id,
                        item,
                        CurrentEmployeeId
                    );


                return Ok(
                    MapToDto(updatedItem)
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
            catch (InvalidOperationException ex)
            {
                return Content(
                    HttpStatusCode.Conflict,
                    ex.Message
                );
            }
        }


        private ItemDto MapToDto(
            Item item)
        {
            return new ItemDto
            {
                ItemId =
                    item.ItemId,

                CategoryId =
                    item.CategoryId,

                BrandId =
                    item.BrandId,

                FormulaId =
                    item.FormulaId,

                ItemName =
                    item.ItemName,

                Description =
                    item.Description,

                PictureUrl =
                    item.PictureUrl,

                Barcode =
                    item.Barcode,

                IsPrescriptionRequired =
                    item.IsPrescriptionRequired,

                SellingPrice =
                    item.SellingPrice,

                MinimumStockLevel =
                    item.MinimumStockLevel,

                RackNumber =
                    item.RackNumber,

                ShelfNumber =
                    item.ShelfNumber,

                LaneNumber =
                    item.LaneNumber,

                IsActive =
                    item.IsActive,

                CreatedAt =
                    item.CreatedAt,

                UpdatedAt =
                    item.UpdatedAt
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