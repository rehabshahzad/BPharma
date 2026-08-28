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
    [Authorize(Roles = "Admin, Pharmacist")]
    [RoutePrefix("api/sales")]
    public class SaleController : ApiController
    {
     
        private readonly ISaleService _service;

        


        public SaleController(ISaleService service)
        {
           _service=    service;
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