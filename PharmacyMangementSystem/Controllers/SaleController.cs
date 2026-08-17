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
    [RoutePrefix("api/sales")]
    public class SaleController : ApiController
    {
        private readonly PharmacyDbContext _context;
        private readonly ISaleService _service;

        private const int CurrentEmployeeId = 1;


        public SaleController()
        {
            _context = new PharmacyDbContext();

            ISaleRepository repository =
                new SaleRepository(_context);

            _service =
                new SaleService(repository);
        }


        // GET api/sales
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            var sales =
                _service.GetAllSales();

            var result =
                sales.Select(s => MapToDto(s))
                     .ToList();

            return Ok(result);
        }


        // GET api/sales/1
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            try
            {
                var sale =
                    _service.GetSaleById(id);

                return Ok(
                    MapToDto(sale)
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


        // POST api/sales
        [HttpPost]
        [Route("")]
        public IHttpActionResult Create(SaleDto dto)
        {
            if (dto == null)
            {
                return BadRequest(
                    "Sale data is required."
                );
            }

            try
            {
                var sale = new Sale
                {
                    CustomerId =
                        dto.CustomerId,

                    AdditionalCharges =
                        dto.AdditionalCharges,

                    Notes =
                        dto.Notes,

                    Status =
                        dto.Status
                };


                var items =
                    dto.Items?
                        .Select(i =>
                            new SaleItem
                            {
                                ItemId =
                                    i.ItemId,

                                OrderedQuantity =
                                    i.OrderedQuantity
                            })
                        .ToList();


                var created =
                    _service.CreateSale(
                        sale,
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


        // PUT api/sales/1
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Update(
            int id,
            SaleDto dto)
        {
            if (dto == null)
            {
                return BadRequest(
                    "Sale data is required."
                );
            }

            try
            {
                var sale =
                    new Sale
                    {
                        AdditionalCharges =
                            dto.AdditionalCharges,

                        Notes =
                            dto.Notes,

                        Status =
                            dto.Status
                    };


                var updated =
                    _service.UpdateSale(
                        id,
                        sale,
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


        private SaleDto MapToDto(
            Sale sale)
        {
            return new SaleDto
            {
                SaleId =
                    sale.SaleId,

                CustomerId =
                    sale.CustomerId,

                AdditionalCharges =
                    sale.AdditionalCharges,

                SubTotalAmount =
                    sale.SubtotalAmount,

                TotalAmount =
                    sale.TotalAmount,

                Status =
                    sale.Status,

                SaleDate =
                    sale.SaleDate,

                CreatedAt =
                    sale.SoldAt,

                UpdatedAt =
                    sale.UpdatedAt,

                Notes =
                    sale.Notes,

                Items =
                    sale.SaleItems?
                        .Select(si =>
                            new SaleItemDto
                            {
                                SaleItemId =
                                    si.SaleItemId,

                                ItemId =
                                    si.ItemId,

                                OrderedQuantity =
                                    si.OrderedQuantity,

                                UnitSalePrice =
                                    si.UnitSalePrice
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