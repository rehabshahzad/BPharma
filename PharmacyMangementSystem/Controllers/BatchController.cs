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
    [Authorize]
    [RoutePrefix("api/batches")]
    public class BatchController : ApiController
    {
        
        private readonly IBatchService _service;

        


        public BatchController(IBatchService service)
        {
            _service = service;
        }


        // GET api/batches
        [HttpGet]
        [Authorize(Roles = "Admin, Pharmacist, Inventorymanager")]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            var batches =
                _service.GetAllBatches();

            var result =
                batches
                    .Select(b => MapToDto(b))
                    .ToList();

            return Ok(result);
        }


        // GET api/batches/1
        [HttpGet]
        [Authorize(Roles = "Admin, Pharmacist, Inventorymanager")]
        [Route("{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            try
            {
                var batch =
                    _service.GetBatchById(id);

                return Ok(
                    MapToDto(batch)
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


        // POST api/batches
        [HttpPost]
        [Authorize(Roles = "Admin, Inventorymanager")]
        [Route("")]
        public IHttpActionResult Create(BatchDto dto)
        {
            if (dto == null)
            {
                return BadRequest(
                    "Batch data is required."
                );
            }

            try
            {
                var batch =
                    new Batch
                    {
                        PurchaseItemId =
                            dto.PurchaseItemId,

                        BatchNumber =
                            dto.BatchNumber,

                        ReceivedQuantity =
                            dto.ReceivedQuantity,

                        ManufacturingDate =
                            dto.ManufacturingDate,

                        ExpiryDate =
                            dto.ExpiryDate
                    };


                var created =
                    _service.CreateBatch(
                        batch,
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


        // PUT api/batches/1
        [HttpPut]
        [Authorize(Roles = "Admin,Inventorymanager")]
        [Route("{id:int}")]
        public IHttpActionResult Update(
            int id,
            BatchDto dto)
        {
            if (dto == null)
            {
                return BadRequest(
                    "Batch data is required."
                );
            }

            try
            {
                var batch =
                    new Batch
                    {
                        PurchaseItemId =
                            dto.PurchaseItemId,

                        BatchNumber =
                            dto.BatchNumber,

                        ReceivedQuantity =
                            dto.ReceivedQuantity,

                        ManufacturingDate =
                            dto.ManufacturingDate,

                        ExpiryDate =
                            dto.ExpiryDate,

                        Status =
                            dto.Status
                    };


                var updated =
                    _service.UpdateBatch(
                        id,
                        batch,
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


        private BatchDto MapToDto(Batch batch)
        {
            return new BatchDto
            {
                BatchId =
                    batch.BatchId,

                PurchaseItemId =
                    batch.PurchaseItemId,

                Status =
                    batch.Status,

                BatchNumber =
                    batch.BatchNumber,

                ReceivedQuantity =
                    batch.ReceivedQuantity,

                ManufacturingDate =
                    batch.ManufacturingDate,

                ExpiryDate =
                    batch.ExpiryDate,

                ReceivedDate =
                    batch.ReceivedDate,

                CreatedAt =
                    batch.CreatedAt,

                UpdatedAt =
                    batch.UpdatedAt
            };
        }
        private int GetCurrentEmployeeId()
        {
            var identity = User.Identity as ClaimsIdentity;

            var claim = identity?.FindFirst("EmployeeId");//look into logged-in user's claims and find the one called EmployeeId

            if (claim == null)
            {
                throw new UnauthorizedAccessException(
                    "Employee ID claim not found."
                );
            }

            return int.Parse(claim.Value); //claim.value is a string but we need an int
        }

        
    }
}