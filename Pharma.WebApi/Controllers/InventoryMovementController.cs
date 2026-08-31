using Pharma.BLL.DTOs;
using Pharma.BLL.Services;
using Pharma.Dal.Repositories;
using Pharma.DAL.Context;
using Pharma.Entity.Entities;
using Pharma.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web.Http;

namespace PharmacyMangementSystem.Controllers
{     
    [RoutePrefix("api/inventory-movements")]
    [Authorize]
    public class InventoryMovementController : ApiController
    {
       
        private readonly IInventoryMovementService _service;

        public InventoryMovementController( IInventoryMovementService service)
        {
            _service = service;
        }

        [Authorize( Roles ="Admin,Pharmacist, InventoryManager")]
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


        [Authorize(Roles = "Admin,Pharmacist, InventoryManager")]
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

        [Authorize(Roles = "Admin,Pharmacist, InventoryManager")]
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

        [Authorize(Roles = "Admin,InventoryManager")]
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
                        GetCurrentEmployeeId()
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