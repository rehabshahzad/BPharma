using Pharma.BLL.Services;
using Pharma.DAL.Context;
using Pharma.Dal.Repositories;
using Pharma.Entity.Entities;
using PharmacyMangementSystem.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;


namespace PharmacyMangementSystem.Controllers
{
    [RoutePrefix("api/purchases")]
    public class PurchaseController : ApiController
    {
        private readonly PharmacyDbContext _context;
        private readonly IPurchaseService _service;
        private const int CurrentEmployeeId = 1;

        public PurchaseController()
        {
            _context = new PharmacyDbContext();
            IPurchaseRepository _repo = new PurchaseRepository(_context);
            _service = new PurchaseService(_repo);

        }
        //GET api/purchases
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            var purchases = _service.GetAllPurchases();
            var result = purchases.Select(p => MapToDto(p)).ToList();

            return Ok(result);
        }

        //GET api/purchases/id
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            try
            {
                var purchase = _service.GetPurchaseById(id);
                return Ok(MapToDto(purchase));

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

        //POST api/purchases/id
        [HttpPost]
        [Route("")]
        public IHttpActionResult Create(PurchaseDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Purchase data is required");
            }
            try
            {
                var purchase = new Purchase
                {
                    SupplierId = dto.SupplierId,
                    AdditionalCharges = dto.AdditionalCharges,
                    Notes = dto.Notes,
                    Status = dto.Status
                };
                var items = dto.Items?.Select(i => new PurchaseItem
                {
                    ItemId = i.ItemId,
                    OrderedQuantity = i.OrderedQuantity,
                    UnitPurchasePrice = i.UnitPurchasePrice

                }).ToList();

                var created = _service.CreatePurchase( 
                    purchase, items, CurrentEmployeeId);

                return Content(HttpStatusCode.Created, MapToDto(created));

            
            }
            catch(ArgumentException ex)
            {return BadRequest(ex.Message);
                
            }
            catch(KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(InvalidOperationException ex)
            {
                return Content(HttpStatusCode.Conflict, ex.Message);
            }
        }

        //PUT: api/purchases/id
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Update(int id, PurchaseDto dto) //controller 
        { if (dto == null)
            {
                return BadRequest("Purchase data is required");
            }

            try
            {
                var purchase = new Purchase
                {
                    AdditionalCharges = dto.AdditionalCharges,
                    Notes = dto.Notes,
                    Status = dto.Status
                };
                var updated = _service.UpdatePurchase(
                    id, purchase, CurrentEmployeeId);
                return Ok(MapToDto(updated));


            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            catch(InvalidOperationException ex)
            {
                return Content(HttpStatusCode.Conflict,ex.Message);
            }
        }
        private PurchaseDto MapToDto(Purchase purchase)
        {
            return new PurchaseDto
            {
                PurchaseId = purchase.PurchaseId,
                SupplierId = purchase.SupplierId,
                AdditionalCharges = purchase.AdditionalCharges,
                Notes = purchase.Notes,
                Status = purchase.Status,
                SubtotalAmount = purchase.SubtotalAmount,
                TotalAmount = purchase.TotalAmount,
                CreatedAt = purchase.CreatedAt,
                UpdatedAt = purchase.UpdatedAt,
                Items =
                    purchase.PurchaseItems?
                        .Select(pi =>
                            new PurchaseItemDto
                            {
                                PurchaseItemId =
                                    pi.PurchaseItemId,

                                ItemId =
                                    pi.ItemId,

                                OrderedQuantity =
                                    pi.OrderedQuantity,

                                UnitPurchasePrice =
                                    pi.UnitPurchasePrice
                            })
                        .ToList()

            };
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);

        }
    }
}