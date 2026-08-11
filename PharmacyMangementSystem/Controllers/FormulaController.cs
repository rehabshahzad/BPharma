using Pharma.BLL.Services;
using Pharma.DAL.Context;
using Pharma.Dal.Repositories;
using Pharma.Entity.Entities;
using PharmacyMangementSystem.DTOs.Formula;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace PharmacyMangementSystem.Controllers
{
    [RoutePrefix("api/formulas")]
    public class FormulaController : ApiController
    {
        private readonly PharmacyDbContext _context;
        private readonly IFormulaService _service;

        // Temporary until JWT/Auth is implemented
        private const int CurrentEmployeeId = 1;


        public FormulaController()
        {
            _context = new PharmacyDbContext();

            IFormulaRepository formulaRepository =
                new FormulaRepository(_context);

            _service =
                new FormulaService(formulaRepository);
        }


        // GET: api/formulas
        [HttpGet]
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
                        CurrentEmployeeId
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
                        CurrentEmployeeId
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