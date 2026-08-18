using Pharma.BLL.DTOs;
using Pharma.BLL.Services;
using Pharma.DAL.Context;
using Pharma.Dal.Repositories;
using Pharma.Entity.Entities;
using Pharma.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace PharmacyMangementSystem.Controllers
{
    [RoutePrefix("api/inventory-movements")]
    public class InventoryMovementController : ApiController
    {
        private readonly PharmacyDbContext _context;
        private readonly IInventoryMovementService _service;

        private const int CurrentEmployeeId = 1;


        public InventoryMovementController()
        {
            _context =
                new PharmacyDbContext();

            IInventoryMovementRepository repository =
                new InventoryMovementRepository(
                    _context
                );

            _service =
                new InventoryMovementService(
                    repository
                );
        }


        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            var movements =
                _service.GetAllMovements();

            var result =
                movements
                    .Select(MapToDto)
                    .ToList();

            return Ok(result);
        }


        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            try
            {
                var movement =
                    _service.GetMovementById(id);

                return Ok(
                    MapToDto(movement)
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


        [HttpGet]
        [Route("batch/{batchId:int}")]
        public IHttpActionResult GetByBatch(
            int batchId)
        {
            try
            {
                var movements =
                    _service
                        .GetMovementsByBatchId(
                            batchId
                        );

                var result =
                    movements
                        .Select(MapToDto)
                        .ToList();

                return Ok(result);
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


        [HttpPost]
        [Route("")]
        public IHttpActionResult CreateManualMovement(
            CreateInventoryMovementDto dto)
        {
            if (dto == null)
            {
                return BadRequest(
                    "Movement data is required."
                );
            }

            try
            {
                if (dto.MovementType !=
                        InventoryMovementType.DamagedOut &&
                    dto.MovementType !=
                        InventoryMovementType.AdjustmentIn &&
                    dto.MovementType !=
                        InventoryMovementType.AdjustmentOut)
                {
                    return BadRequest(
                        "This movement type cannot be created manually."
                    );
                }


                var movement =
                    _service.CreateMovement(
                        dto.BatchId,
                        dto.MovementType,
                        dto.Quantity,
                        null,
                        dto.Remarks,
                        CurrentEmployeeId
                    );


                return Content(
                    HttpStatusCode.Created,
                    MapToDto(movement)
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


        private InventoryMovementDto MapToDto(
            InventoryMovement movement)
        {
            return new InventoryMovementDto
            {
                InventoryMovementId =
                    movement.InventoryMovementId,

                BatchId =
                    movement.BatchId,

                MovementType =
                    movement.MovementType,

                QuantityChange =
                    movement.QuantityChange,

                ReferenceId =
                    movement.ReferenceId,

                Remarks =
                    movement.Remarks,

                MovementDate =
                    movement.MovementDate,

                PerformedByEmployeeId =
                    movement.PerformedByEmployeeId
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