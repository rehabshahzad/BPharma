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
    [RoutePrefix("api/batches")]
    public class BatchController : ApiController
    {
        private readonly PharmacyDbContext _context;
        private readonly IBatchService _service;

        private const int CurrentEmployeeId = 1;


        public BatchController()
        {
            _context = new PharmacyDbContext();

            IBatchRepository repository =
                new BatchRepository(_context);

            _service =
                new BatchService(repository);
        }


        // GET api/batches
        [HttpGet]
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


        // PUT api/batches/1
        [HttpPut]
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