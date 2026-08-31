using Pharma.BLL.Services;
using Pharma.Dal.Repositories;
using Pharma.DAL.Context;
using Pharma.Entity.Entities;
using PharmacyMangementSystem.DTOs.Formula;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web.Http;

namespace PharmacyMangementSystem.Controllers
{
    [Authorize]
    [RoutePrefix("api/formulas")]
    public class FormulaController : ApiController
    {
    
        private readonly IFormulaService _service;

        public FormulaController(IFormulaService service)
        {
            _service = service;
        }
        

        // GET: api/formulas
        [HttpGet]
        [Authorize(Roles = "Admin, Pharmacist, InventoryManager")]
        [Route("")]
        public IHttpActionResult GetAllFormulas()
        {
            var formulas = _service
                .GetAllFormulas()
                .Select(f => MapToDto(f))
                .ToList();

            return Ok(formulas);
        }


        // GET: api/formulas/1
        [HttpGet]
        [Authorize(Roles = "Admin, Pharmacist, InventoryManager")]
        [Route("{id:int}")]
        public IHttpActionResult GetFormulaById(int id)
        {
            try
            {
                var formula =
                    _service.GetFormulaById(id);

                return Ok(
                    MapToDto(formula)
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


        // POST: api/formulas
        [HttpPost]
        [Authorize(Roles = "Admin, InventoryManager")]
        [Route("")]
        public IHttpActionResult CreateFormula(
            FormulaDto request)
        {
            if (request == null)
            {
                return BadRequest(
                    "Formula data is required."
                );
            }

            try
            {
                var formula = new Formula
                {
                    FormulaName =
                        request.FormulaName
                };

                var createdFormula =
                    _service.CreateFormula(
                        formula,
                        GetCurrentEmployeeId()
                    );

                return Content(
                    HttpStatusCode.Created,
                    MapToDto(createdFormula)
                );
            }
            catch (ArgumentException ex)
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


        // PUT: api/formulas/1
        [HttpPut]
        [Authorize(Roles = "Admin,InventoryManager")]
        [Route("{id:int}")]
        public IHttpActionResult UpdateFormula(
            int id,
            FormulaDto request)
        {
            if (request == null)
            {
                return BadRequest(
                    "Formula data is required."
                );
            }

            try
            {
                var updatedFormula = new Formula
                {
                    FormulaName =
                        request.FormulaName,

                    isActive =
                        request.isActive
                };

                var formula =
                    _service.UpdateFormula(
                        id,
                        updatedFormula,
                        GetCurrentEmployeeId()
                    );

                return Ok(
                    MapToDto(formula)
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


        private FormulaDto MapToDto(
            Formula formula)
        {
            return new FormulaDto
            {
                FormulaId =
                    formula.FormulaId,

                FormulaName =
                    formula.FormulaName,

                isActive =
                    formula.isActive,

                CreatedAt =
                    formula.CreatedAt,

                UpdatedAt =
                    formula.UpdatedAt
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